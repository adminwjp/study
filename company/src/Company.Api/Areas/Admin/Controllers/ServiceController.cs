using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class ServiceController : BaseController<ServiceInfo>
    {
        public ServiceController(IRepository<ServiceInfo> repository, ILogger<ServiceController> logger) : base(repository, logger)
        {
        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] ServiceInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "img")
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                using Stream stream= file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomUtils.Instance.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Core.UploadDirectory+"\\"+Core.UploadService + "\\" + name, buffer);
                obj.Img = new ImageInfo()
                {
                    Name = RandomUtils.Instance.Id,
                    Href = $"{RandomUtils.Instance.Id}.{suffix}",
                    Src = name,
                    Create= true,
                    Type = Core.Service
                }; 
                base.Repository.Save();
            }
            else
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
            }
            this.AddMiddleExecet(obj);
            obj.CreateDate = DateTime.Now;
            base.Repository.Add(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] ServiceInfo obj)
        {
            if (Request.ContentType != null)
            {
                if (Request.ContentType.Contains("application/json"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Ref(ref obj, reader.ReadToEnd());
                    }
                }
                else if (Request.ContentType.Contains("text/xml"))
                {
                    using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                    Type t = typeof(ServiceInfo);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as ServiceInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "img")
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomUtils.Instance.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Environment.CurrentDirectory+"\\"+Core.UploadService + "\\" + name, buffer);
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).FirstOrDefault();
                if (obj.Img == null || !obj.Img.Id.HasValue)
                {
                    obj.Img = old.Img;
                }
                obj.CreateDate = old.CreateDate;
                if (obj.Img != null)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadService + "\\" + obj.Img.Src);
                    obj.Img.Name = RandomUtils.Instance.Id;
                    obj.Img.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                    obj.Img.Src = name;
                    (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.Img);
                }
                else
                {
                    obj.Img = new ImageInfo()
                    {
                        Name = RandomUtils.Instance.Id,
                        Href = $"{RandomUtils.Instance.Id}.{suffix}",
                        Src = name,
                        Type = Core.Service,
                        Create = true
                    };
                    (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Img);
                }
                base.Repository.Save();
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).FirstOrDefault();
                if (obj.Img == null || !obj.Img.Id.HasValue)
                {
                    obj.Img = old.Img;
                }
                obj.CreateDate = old.CreateDate;
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
            obj.ModifyDate = DateTime.Now;
            base.Repository.Update(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
        }
        protected override void AddMiddleExecet(ServiceInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = ((Company.Domain.CompanyDbContext)base.Repository.DbContext).Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(ServiceInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<ServiceInfo> QueryList(ServiceInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it=>it.Category).Include(it=>it.Img)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
        [HttpGet("category")]
        public virtual async Task<Utility.ResponseApi> Category()
        {
            Utility.ResponseApi responseApi = ResponseApiUtils.Success(GetLanguage());
            var data = base.Repository.Find(it => it.Enable.HasValue && it.Enable.Value).Select(it => new ServiceInfo()
            {
                Id=it.Id,
                Name=it.Name,
                EnglishName=it.EnglishName
            });
            responseApi.Data = data;
            return await Task.FromResult<Utility.ResponseApi>(responseApi);
        }
    }
}