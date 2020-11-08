using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NHibernate.Criterion;
using NHibernate.Linq;
using SocialContact.Api.Data;
using SocialContact.Api.Models;
using StackExchange.Redis;
using Utility;
using Utility.Response;
using Utility.Domain.Uow;
using Utility.Redis;
using Utility.Enums;
using Utility.Base64;
using Utility.Randoms;
using Utility.Security.Extensions;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    /// <summary>
    /// 上传 单独上传 或一起提交
    /// </summary>
    [Area("admin")]
    // [Route("admin/api/v1/[controller]")]
    [Route("admin/api/v1")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    public class UserFileController : SocialContact.Api.Controllers.BaseController<SocialContact.Domain.Core.UserFileInfo, QueryUserFileFormViewModel, QueryUserFileInfoResultViewModel>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserFileService _userFileService;
        private readonly Core _core;
        private readonly string _fileAddress;
        public UserFileController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IConfiguration configuration, IMemoryCache cache, AuthrizeValidator authrize, 
            Core core, IUserFileService userFileService, IHttpClientFactory httpClientFactory, ILogger<UserFileController> logger) :base(redisCache,unitWork, configuration, cache,authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            this._userFileService = userFileService;
            this._core = core;
            this._httpClientFactory = httpClientFactory;
            this._fileAddress = Configuration["FileAddress"];
            base.IsCustomValidator = true;
            base.PageName = "file";
        }
        [HttpPost("file/add")]
        public override async Task<ResponseApi> Add([FromForm]SocialContact.Domain.Core.UserFileInfo obj)
        {
            return await base.Add(obj);
  
        }
        protected override async Task<ResponseApi> AddMiddlewareExecuted(UserFileInfo obj)
        {
            base.GetBase64(obj);
            if (string.IsNullOrEmpty(obj.Base64))
            {
                ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileFail,false);
                return await Task.FromResult(response1); 
            }
            obj = this.AddMiddlewareExecute(obj);
            var parent = base.UnitWork.FindSingle<UserFileInfo>(it => it.Base64 == obj.Base64);
            if (parent == null)
            {
                obj.Id = (int)base.UnitWork.Insert(obj);
                System.IO.File.WriteAllBytes($"{AddressConfig.UploadImgDirectory}\\{ obj.Src}", /*obj.Base64*/Base64Helper.FromBase64String(obj.Base64));
                obj = (UserFileInfo)obj.Clone();
            }
            else
            {
                obj.Parent = parent;
                obj.Base64 = null;
            }
            obj.FileId = RandomHelper.OrderId.Sha1();
            obj.Src = $"{RandomHelper.OrderId.Sha1()}.{obj.Parent.Src.Split('.').LastOrDefault()}";
            obj.Id = (int)base.UnitWork.Insert(obj);
            var userFileEntry = this._userFileService.ToUserFileEntry(obj);
            this._userFileService.Publish(userFileEntry);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.AddSuccess);
            response.Data  = new { obj.Src, Id = obj.FileId };
            return await Task.FromResult(response);
        }
        protected override async Task<ResponseApi> Validate(SocialContact.Domain.Core.UserFileInfo obj, int way = 0)
        {
            obj.FileId = obj.Src;
            var error =await base.Validate(obj, way);
            if (error == null || error.Data == null)
            {
                Dictionary<string, List<string>> customErrors = new Dictionary<string, List<string>>();
                if (Request.Form.Files.Count == 1)
                {
                    if (Request.Form.Files[0].Name.ToLower() != "file")
                    {
                        customErrors.Add("file", new List<string>() { "file参数错误" });
                    }
                    string suffix = Request.Form.Files[0].FileName.Split('.').LastOrDefault();
                    //if (!base.Test && !obj.Type.Contains(suffix))
                    //{
                    //    customErrors.Add("type", new List<string>() { "上传文件格式错误" });
                    //}
                }else if (Request.Form.Files.Count == 0&& string.IsNullOrEmpty(obj.FileId))
                {
                    customErrors.Add("file_id", new List<string>() { "file_id 参数不能为空" });
                }
                else if(Request.Form.Files.Count>1)
                {
                    
                    customErrors.Add("file", new List<string>() { "上传文件失败" });
                }
                return await Task<ResponseApi>.FromResult(customErrors.Count > 0 ? new ResponseApi() { Message = "参数错误", Code = (int)Code.参数错误, Data = new { Error = customErrors } } : null);
            }
     
            return error;
        }
    
        [HttpPost("file/edit")]
        public override async  Task<ResponseApi> Edit([FromForm] SocialContact.Domain.Core.UserFileInfo obj)
        {
            return await base.Edit(obj);
        }
        protected override async Task<ResponseApi> EditMiddlewareExecuted(UserFileInfo obj)
        {
            obj.UpdateDate = DateTime.Now;
            base.GetBase64(obj);
            this.EditMiddlewareExecute(obj);
            if (string.IsNullOrEmpty(obj.Base64))
            {
                var oldObj= base.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it => it.Id == obj.Id && it.FileId == obj.FileId);
                if (oldObj == null)
                {
                    ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileFail, false);
                    return await Task.FromResult(response1);
                }
                oldObj.FileId = RandomHelper.OrderId.Sha1();
                oldObj.Src = $"{RandomHelper.OrderId.Sha1()}.{oldObj.Src.Split('.').LastOrDefault()}";
                obj.FileId = oldObj.FileId;
                obj.Src = oldObj.Src;
                obj.Parent = oldObj.Parent;
                base.UnitWork.Update<UserFileInfo>(it => it.Id == obj.Id, it => new UserFileInfo()
                {
                    UpdateDate = DateTime.Now,
                    Admin = obj.Admin,
                    Category = obj.Category,
                    Description = obj.Description,
                    FileId = obj.FileId,
                    Src = obj.Src
                }) ;
            }
            else
            {
                var parent = base.UnitWork.FindSingle<UserFileInfo>(it => it.Base64 == obj.Base64);
                if (parent == null)
                {
                    parent = (UserFileInfo)obj.Clone();
                    parent.Parent = null;
                    parent.Base64 = obj.Base64;
                    parent.Id = (int)base.UnitWork.Insert(parent);
                    System.IO.File.WriteAllBytes($"{AddressConfig.UploadImgDirectory}\\{ parent.Src}",Base64Helper.FromBase64String(parent.Base64));
                }
                obj.Base64 = null;
                obj.Parent = parent;
                obj.FileId = RandomHelper.OrderId.Sha1();
                obj.Src = $"{RandomHelper.OrderId.Sha1()}.{obj.Parent.Src.Split('.').LastOrDefault()}";
                base.UnitWork.Update(obj);
            }
            var userFileEntry = this._userFileService.ToUserFileEntry(obj);
            this._userFileService.Publish(userFileEntry);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ModifySuccess);
            response.Data = new { obj.Src, Id = obj.FileId };
            return await Task.FromResult(response);
        }

        protected override SocialContact.Domain.Core.UserFileInfo AddMiddlewareExecute(SocialContact.Domain.Core.UserFileInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            obj.Category = obj.Category == null || !obj.Category.Id.HasValue ? null : obj.Category;
            return obj;
        }
        protected override SocialContact.Domain.Core.UserFileInfo EditMiddlewareExecute(SocialContact.Domain.Core.UserFileInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            obj.Category = obj.Category == null || !obj.Category.Id.HasValue ? null : obj.Category;
            return obj;
        }
        [HttpPost("file/upload")]
        public async Task<ResponseApi> Upload([FromForm] IFormCollection froms)
        {
            if (froms.Count != 1)
            {
                ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileFail, false);
                return await Task.FromResult(response1);
            }
            string type = Request.Form["type"];
            return await Upload(froms, type);
        }
        async Task<ResponseApi> Upload([FromForm] IFormCollection froms,string type,string name="",bool ignore=true)
        {
            if (froms.Count != 1||(!ignore && Request.Form.Files[0].Name.ToLower() != name))
            {
                ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileFail, false);
                return await Task.FromResult(response1);
            }
            using System.IO.Stream stream = Request.Form.Files[0].OpenReadStream();
            byte[] buffer = new Byte[stream.Length];
            stream.Read(buffer);
            var id = RandomHelper.OrderId.Sha1();
            string suffix = Request.Form.Files[0].FileName.Split('.').LastOrDefault();
            //if (!base.Test && !type.Contains(suffix))
            //{
            //    return await Task.FromResult(new Utility.Response() { Message = "上传文件格式错误!", Success = false, Code = (int)Code.上传文件格式错误 });
            //}
            var src = $"{RandomHelper.OrderId.Sha1()}.{suffix}";
            System.IO.File.WriteAllBytes($"{AddressConfig.UploadImgDirectory}\\{src}", buffer);
            if (base.RedisCache.AddHash("file", id, $"{src}_{type ?? string.Empty}"))
            {
                ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileSuccess, false);
                response1.Data = new { Id = id, src };
                return await Task.FromResult(response1);
            }
            else
            {
                ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.UploadFileFail, false);
                return await Task.FromResult(response1);
            }
        }

        [HttpPost("file/delete")]
        public override async Task<ResponseApi> Delete([/*Bind(new[] { "ids" }),*/ FromForm,FromBody] DeleteEntry deleteEntry)
        {
            return await base.Delete(deleteEntry);
        }
        [HttpGet("file/xml")]
        public  async Task<ResponseApi> Xml()
        {
            int[] Ids = new int[] { 1, 2, 3 };
            using StringWriter sw = new StringWriter();
            Type t = Ids.GetType();
            XmlSerializer serializer = new XmlSerializer(t);
            serializer.Serialize(sw, Ids);
            sw.Close();
            ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.OperatorSuccess, true);
            response1.Data = sw.ToString();
            return await Task.FromResult(response1);
        }
        [HttpGet("file/delete/{id}")]
        public override async Task<ResponseApi> Delete(int? id)
        {
            return await base.Delete(id);
        }
        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("file/get/{id}")]
        public IActionResult Get(string id)
        {
            return Images(id);
        }
        [HttpPost("file/query")]
        public override Task<ResponseApi> Query([FromForm,FromBody] QueryUserFileFormViewModel query, int? page, int? size)
        {
            return base.Query(query, page, size);
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserFileFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (obj.CategoryId.HasValue)
            {
                criterias.Add(Expression.Eq("Category.Id", obj.CategoryId.Value)); 
                res = true;
            }
            return res;
        }
        protected override bool QueryFilterByAnd(List<AbstractCriterion> criterias, QueryUserFileFormViewModel obj)
        {
            criterias.Add(Expression.NotEqProperty("Parent.Id", "Id"));
            return true;
        }
        protected override List<SocialContact.Domain.Core.UserFileInfo> DataParseIfWhileReference(List<SocialContact.Domain.Core.UserFileInfo> data, bool where = false)
        {
            //去引用 防止automapper 映射失败
            foreach (var item in data)
            {
                item.Parent = null;
            }
            return data;
        }
        protected override IQueryable<SocialContact.Domain.Core.UserFileInfo> QueryChildFilter(IQueryable<SocialContact.Domain.Core.UserFileInfo> query, QueryUserFileFormViewModel obj)
        {
            query = query.Where(it=>it.Parent.Id!=it.Id);
            return query;
        }
        /// <summary>
        /// 查看文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("img/{id}")]
        public IActionResult Images(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var fileCategoryEntries = Cache.Get<List<Api.Models.FileCategoryEntry>>(Core.FileCategoryChannel);
            if (!base.Test&& fileCategoryEntries!=null&&!fileCategoryEntries.Exists(it =>it.Id== AddressConfig.ImgId && it.Sufixs.Contains(id.Split('.').LastOrDefault())))
            {
                return NotFound();
            }
            string src = $"{AddressConfig.UploadImgDirectory}\\{id}";
            if (System.IO.File.Exists(src))
            {
                return new FileContentResult(System.IO.File.ReadAllBytes(src), "*/*");
            }
            src = base.RedisCache.GetHashValue("file", id);
            if (!string.IsNullOrEmpty(src))
            {
                src = src.Split('_')[0];
                src = $"{AddressConfig.UploadImgDirectory}\\{src}";
                if (System.IO.File.Exists(src))
                {
                    return new FileContentResult(System.IO.File.ReadAllBytes(src), "*/*");
                }
            }
            var data = Cache.Get<List<SocialContact.Api.Models.UserFileEntry>>(Core.FileChannel);
            if (data!=null&&data.Any())
            {
                int.TryParse(id, out int intId);
                var userFileEntry = data.Find(it => it.FileId == intId || it.FileName==id|| it.FileSrc == id);
                if (userFileEntry != null)
                {
                    return new FileContentResult(System.IO.File.ReadAllBytes(AddressConfig.UploadImgDirectory + "\\" + userFileEntry.AbstractUrl), "*/*");
                }
            }
            return NotFound();
        }
        [HttpPost("img/upload")]
        public async Task<ResponseApi> UploadHeadPic([FromForm] IFormCollection froms)
        {
            return await Upload(froms);
        }

        [HttpGet("file/operator")]

        public override async Task<ResponseApi> Operator()
        {
            return await base.Operator();
        }
        [HttpGet("file/category")]

        public async Task<ResponseApi> QueryCategory()
        {
            List<CategoryEntry> categoryEntries = UnitWork.Find<SocialContact.Domain.Core.UserFileInfo>(it=>it.Parent.Id.HasValue).Select(Select()).ToList();
            ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response1.Data = categoryEntries;
            return await Task.FromResult(response1);
        }
        protected override Func<SocialContact.Domain.Core.UserFileInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Src };
        }
        [HttpGet("file/getdata")]
        public ResponseApi GetData()
        {
            ResponseApi response1 = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response1.Data = base.Cache.Get(Core.FileChannel);
            return response1;
        }
    }
}