using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using SocialContact.Domain.ViewModel;
using Utility;
using SocialContact.Api.Data;
using Microsoft.Extensions.Caching.Memory;
using SocialContact.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using NHibernate.Criterion;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Xml.Serialization;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]
    public class AdminController : SocialContact.Api.Controllers.BaseController<AdminInfo, QueryAdminFormViewModel, QueryAdminInfoResultViewModel>
    {
        private readonly Core _core;
        private readonly IUserFileService _userFileService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fileAddress;
        public AdminController(RedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, Core core, IUserFileService userFileService,
            AuthrizeValidator authrize, IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AdminController> Logger) : base(redisCache, unitWork, configuration,cache, authrize, Logger)
        {
            this._core = core;
            this._userFileService = userFileService;
            this._httpClientFactory = httpClientFactory;
            this._fileAddress = Configuration["FileAddress"];
            base.IsCustomValidator = true;
            base.PageName = "admin";
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpPost("login")]
        public async Task<Utility.ResponseApi> Login(string returnUrl, [FromForm]/*[FromBody]*/ LoginViewModel login)
        {
			if (Request.ContentType.Contains("application/json"))
            {	
				using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
					// Ref(ref login, reader.ReadToEnd());
                    Ref(ref login, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {		
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(LoginViewModel);
                XmlSerializer serializer = new XmlSerializer(t);
                login = serializer.Deserialize(reader) as LoginViewModel;
            }
            //base.ActionParam(HttpContext.Request,ref login);//无效 作用域可能 绑定模型失败
            var error = ArgumentsUtils.CheckError(typeof(LoginViewModel), login);
            if (error != null) return await Task.FromResult(error);
            login.Password = login.Password.Sha1();
            var admin = UnitWork.FindSingle<AdminInfo>(it => (it.Account == login.Account || it.Email == login.Account || it.Phone == login.Account)
             && it.Password == login.Password);
            if (admin == null)
            {
                Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.LoginFail,false);
                return await Task.FromResult(response);
            }
            else
            {
                Logger.LogInformation("登录成功!");
                var date = DateTime.Now;
                var token = $"{admin.Account}{admin.Password}{date.ToString("yyyyMMddHHmmssfff")}".Sha1();
                string key = $"{admin.Account}_{RandomUtils.Instance.OrderId}".AesEncrypt(Core.AesKey, Core.AesIv);
                string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                admin.LoginIp = ip;
                admin.LoginIps = admin.LoginIps ?? new HashSet<string>(50);
                admin.LoginIps.Add(ip);
                admin.Token = token;
                admin.LoginDate = date;
                admin.ExpressIn = 24 * 60 * 60;
                UnitWork.Update(admin);
                var value = RedisCache.HashGet("accounts", admin.Account);
                if (!string.IsNullOrEmpty(value))
                {
                    Logger.LogInformation($"该账户{admin.Account}登录成功,之前token信息缓存过,目前移除之前token信息!");
                    if (RedisCache.Remove(value))
                    {
                        Logger.LogWarning($"该账户{admin.Account}之前已登录过,缓存未过期,移除成功,移除token为：{value}");
                    }
                    else
                    {
                        Logger.LogInformation($"该账户{admin.Account}之前已登录过,缓存未过期,移除失败,移除token为：{value}");
                    }
                }
                else
                {
                    Logger.LogInformation($"该账户{admin.Account}登录成功,之前token {token}未缓存过或过期!");
                }
                string tokenJson = admin.ToJson();
                //var cache = HttpContext.RequestServices.GetService<IDistributedCache>();
                //cache.SetString(token, tokenJson, new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.Now.AddHours(24) });
                if (base.RedisCache.AddString(token, tokenJson, date.AddHours(24).TimeOfDay, When.NotExists))
                {
                    base.Logger.LogInformation($"该账户{admin.Account}登录成功,token {token}缓存成功,token信息为：{tokenJson}");
                }
                else
                {
                    base.Logger.LogWarning($"该账户{admin.Account}登录成功,token {token}缓存失败,token信息为：{tokenJson}");
                    Utility.ResponseApi response = ResponseApiUtils.Error(GetLanguage());
                    return await Task.FromResult(response);
                }
                if (base.RedisCache.HashSet("accounts", new HashEntry[] { new HashEntry(admin.Account, token) }))
                {
                    base.Logger.LogInformation($"该账户{admin.Account}登录成功,缓存token {token}成功,用于移除未过期的token信息");
                    //var data = base.Cache.Get<AdminInfo>(admin.Token);
                    //if (data == null)
                    //{
                    //    base.Cache.Set<AdminInfo>(admin.Token, admin, DateTimeOffset.Now.AddHours(24));
                    //}
                    base.Cache.Set<AdminInfo>(admin.Token, admin, DateTimeOffset.Now.AddHours(24));
                    Response.Headers.Add("token", token.AesEncrypt(Core.AesKey, Core.AesIv));
                    Response.Headers.Add("id", admin.Account);
                    Response.Headers.Add("key", key);
                    HttpContext.Response.Cookies.Append("token", token, new CookieOptions() { SameSite = SameSiteMode.Lax, IsEssential = true });
                    HttpContext.Response.Cookies.Append("userid", admin.Account.AesEncrypt(Core.AesKey, Core.AesIv), new CookieOptions() { SameSite = SameSiteMode.Lax, IsEssential = true });
                    HttpContext.Response.Cookies.Append("k", key.AesEncrypt(Core.AesKey, Core.AesIv), new CookieOptions() { SameSite = SameSiteMode.Lax,IsEssential=true });
                    Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.LoginSuccess);
                    response.Data = new { Token = token, ExpressIn = 24 * 3600 };
                    // var claims = new List<Claim>
                    // {
                        // new Claim("user", admin.Account),
                        // new Claim("role", "Member")
                    // };
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));
                    return await Task.FromResult(response);
                }
                else
                {
                    Logger.LogWarning($"该账户{admin.Account}登录成功,缓存token {token}失败");
                    Utility.ResponseApi response = ResponseApiUtils.Error(GetLanguage());
                    return await Task.FromResult(response);
                }
            }
        }

        [HttpPost("logout")]
        public async Task<Utility.ResponseApi> Logout()
        {
            AdminInfo admin = GetAdminInfo();
            if (base.RedisCache.Remove(admin.Token))
            {
                base.Logger.LogInformation($"该账户{admin.Account}退出成功,删除token {admin.Token}成功");
                // return await Task.FromResult(new Utility.Response() { Message = "退出成功!", Code = (int)Code.退出成功 });
            }
            else
            {
                base.Logger.LogWarning($"该账户{admin.Account}登录退出失败,删除token {admin.Token}失败");
                Utility.ResponseApi response = ResponseApiUtils.Error(GetLanguage());
                return await Task.FromResult(response);
            }
            if (RedisCache.HashDelete("accounts", admin.Account))
            {
                base.Logger.LogInformation($"该账户{admin.Account}退出成功,该账户{admin.Account}缓存删除成功,token {admin.Token}");
                Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.LogoutSuccess);
                return await Task.FromResult(response);
            }
            else
            {
                base.Logger.LogWarning($"登录退出失败,该账户{admin.Account}缓存删除失败,token {admin.Token}");
                Utility.ResponseApi response = ResponseApiUtils.Error(GetLanguage());
                return await Task.FromResult(response);
            }
        }
        [HttpGet("reqsk")]
        public async Task<Utility.ResponseApi> Reqsk(string version,string cr,string callback)
        {
            return await Task.FromResult<Utility.ResponseApi>(null );
        }
        protected override async Task<ResponseApi> AddMiddlewareExecuted(AdminInfo obj)
        {
            obj = this.AddMiddlewareExecute(obj);
            var fileInfo = new UserFileInfo();
            UserFileInfo adminHeadPic = null;
            base.GetBase64(fileInfo);
            if (string.IsNullOrEmpty(fileInfo.Base64))
            {
                fileInfo.FileId = obj.HeadPic.FileId;
                adminHeadPic = base.UnitWork.FindSingle<UserFileInfo>(it => it.FileId == fileInfo.FileId&&it.Type=="head_pic");
                if(adminHeadPic==null)
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.UploadFileFail));
                adminHeadPic.FileId = RandomUtils.Instance.OrderId.Sha1();
                adminHeadPic.Src = $"{RandomUtils.Instance.OrderId.Sha1()}.{adminHeadPic.Src.Split('.').LastOrDefault()}";
            }
            else
            {
                var parent = base.UnitWork.FindSingle<UserFileInfo>(it => it.Base64 == fileInfo.Base64);
                if (parent == null)
                {
                    fileInfo.Id = (int)base.UnitWork.Add(fileInfo);
                    System.IO.File.WriteAllBytes($"{AddressConfig.UploadImgDirectory}\\{ fileInfo.Src}", Base64Utils.FromBase64String(fileInfo.Base64));
                    parent = fileInfo;
                }
                adminHeadPic = (UserFileInfo)parent.Clone();
                adminHeadPic.Id = (int)base.UnitWork.Add(adminHeadPic);
            }
            obj = this.AddMiddlewareExecute(obj);
            var userFileEntry = this._userFileService.ToUserFileEntry(adminHeadPic);
            this._userFileService.Publish(userFileEntry);
            obj.HeadPic = adminHeadPic;
            this.UnitWork.Add(obj);
            this.UnitWork.Update<UserFileInfo>(it => it.Id == adminHeadPic.Id, it => new UserFileInfo() { Admin = obj,Type="head_pic" ,FileId=adminHeadPic.FileId,Src=adminHeadPic.Src});
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.AddSuccess);
            return await Task.FromResult(response);
        }
        protected override AdminInfo EditMiddlewareExecute(AdminInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            Set(obj);
            if (!UnitWork.IsExist<AdminInfo>(it => it.Password == obj.Password))
            {
                obj.Password = obj.Password.Sha1();
            }
            return obj;
        }
        protected override async Task<ResponseApi> EditMiddlewareExecuted(AdminInfo obj)
        {
            this.EditMiddlewareExecute(obj);
            obj.UpdateDate = DateTime.Now;
            var oldObj = UnitWork.FindSingle<AdminInfo>(it => it.Id == obj.Id);
            obj = UpdateOldObject(obj, ref oldObj);
            var fileInfo = obj.HeadPic ?? new UserFileInfo() { Admin = obj } ;
            fileInfo.Admin = obj;
            UserFileInfo adminHeadPic = null;
            base.GetBase64(fileInfo);
            if (string.IsNullOrEmpty(fileInfo.Base64))
            {
                adminHeadPic = base.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it => it.Admin.Id == obj.Id && it.FileId == fileInfo.FileId && it.Type == "head_pic");
                if (Request.Form.Files.Count == 0 && adminHeadPic == null)
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.UploadFileFail));
                }
                adminHeadPic.FileId = RandomUtils.Instance.OrderId.Sha1();
                adminHeadPic.Src = $"{RandomUtils.Instance.OrderId.Sha1()}.{adminHeadPic.Parent.Src.Split('.').LastOrDefault()}";
            }
            else
            {
                adminHeadPic = base.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it => it.Admin.Id == obj.Id && it.Type == "head_pic");
                if (adminHeadPic == null)
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                if (adminHeadPic.Parent.Base64 != fileInfo.Base64)
                {
                    var temp= base.UnitWork.FindSingle<SocialContact.Domain.Core.UserFileInfo>(it => it.Base64==fileInfo.Base64);
                    if(temp==null)
                    {
                        adminHeadPic.Parent.Id = (int)base.UnitWork.Add(fileInfo);
                        System.IO.File.WriteAllBytes($"{AddressConfig.UploadImgDirectory}\\{ fileInfo.Src}", Base64Utils.FromBase64String(fileInfo.Base64));
                    }
                    else
                    {
                        adminHeadPic.Parent = temp;
                    }
                }
                adminHeadPic.FileId = RandomUtils.Instance.OrderId.Sha1();
                adminHeadPic.Src = $"{RandomUtils.Instance.OrderId.Sha1()}.{adminHeadPic.Parent.Src.Split('.').LastOrDefault()}";
                base.UnitWork.Update<UserFileInfo>(it => it.Id == adminHeadPic.Id, it => new UserFileInfo() { Parent = adminHeadPic.Parent, FileId= adminHeadPic.FileId,Src= adminHeadPic.Src,UpdateDate=DateTime.Now });
            }
            obj.HeadPic = adminHeadPic;
            var userFileEntry = this._userFileService.ToUserFileEntry(adminHeadPic);
            this._userFileService.Publish(userFileEntry);
            base.UnitWork.Update(obj);
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess);
            return await Task.FromResult(response);
        }
        protected override AdminInfo AddMiddlewareExecute(AdminInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            Set(obj);
            obj.Password = obj.Password.Sha1();
            return obj;
        }
        void Set(AdminInfo obj)
        {
            obj.Parent = base.Test ? obj : base.GetAdminInfo();
        }
        protected override async Task<Dictionary<string, List<string>>> CustomValidate(Dictionary<string, List<string>> errors, AdminInfo obj, int way = 0)
        {
            if (!base.Test&&(obj.Role == null || !obj.Role.Id.HasValue))
            {
                errors.Add("role.id", new List<string>() { "角色id不能为空" });
            }
            if (Request.Form.Files.Count == 1 && Request.Form.Files[0].Name != "head_pic")
            {
                errors.Add("head_pic", new List<string>() { "head_pic 参数不能为空" });
            }
            else if(Request.Form.Files.Count ==0&&  (obj.HeadPic == null || string.IsNullOrEmpty(obj.HeadPic.FileId)))
            {
                errors.Add("head_pic.id", new List<string>() { "用户文件id不能为空" });
            }
            return await base.CustomValidate(errors, obj, way);
        }
        /// <summary>
        /// 实现树形列表查询 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override IQueryable<AdminInfo> QueryChildFilter(IQueryable<AdminInfo> query, QueryAdminFormViewModel obj)
        {
            query = base.QueryChildFilter(query);
            return query;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryAdminFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (this.Query(ref criterias, obj)) res = true;
            return res;
        }
     
        
        protected override Func<AdminInfo, CategoryEntry> Select()
        {
            return it=> new CategoryEntry() { Id = it.Id.Value, Category = it.Account };
        }
        [HttpGet("role_required")]

        public async Task<Utility.ResponseApi> RoleRequired()
        {
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.QuerySuccess);
            response.Data = base.Test ? false : true;
            return await Task.FromResult(response);
        }
        protected override List<AdminInfo> DataParseIfWhileReference(List<AdminInfo> data, bool where = false)
        {
            if (data.Any())
            {
                if (where)
                {
                    return RecursionDataParseByWhere(data);
                }
                else
                {
                    return RecursionDataParse(data, null);
                }
            }
            else
            {
                return data;
            }
        }
        bool Query(ref List<AbstractCriterion> criterias, QueryAdminFormViewModel obj)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(obj.Token))
            {
                criterias.Add(Expression.Like("Token", $"%{obj.Token}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Account))
            {
                criterias.Add(Expression.Like("Account", $"%{obj.Account}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.NickName))
            {
                criterias.Add(Expression.Like("NickName", $"%{obj.NickName}%"));
                res = true;
            }
            if (obj.BirthdayDate != null && obj.BirthdayDate.Length == 2)
            {
                criterias.Add(Expression.Between("Birthday", obj.BirthdayDate[0], obj.BirthdayDate[1]));
                res = true;
            }
            if (obj.RoleId != null && obj.RoleId.HasValue)
            {
                criterias.Add(Expression.Eq("Role.Id", obj.RoleId));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.RealName))
            {
                criterias.Add(Expression.Like("RealName", $"%{obj.RealName}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Phone))
            {
                criterias.Add(Expression.Like("Phone", $"%{obj.Phone}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Sex))
            {
                criterias.Add(Expression.Eq("Sex", obj.Sex));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Email))
            {
                criterias.Add(Expression.Like("Email", $"%{obj.Email}%"));
                res = true;
            }
            if (obj.LoginDate != null && obj.LoginDate.Length == 2)
            {
                criterias.Add(Expression.Between("LoginDate", obj.LoginDate[0], obj.LoginDate[1]));
                res = true;
            }
            return res;
        }
        [HttpGet("rsa")]
        public IActionResult GetRas()
        {
            RSA rSA = RSA.Create();
            return new JsonResult(new {privateKey=rSA.ToXmlString(true),publicKey=rSA.ToXmlString(false) });
        }
        [HttpGet("base64")]
        public IActionResult HexToBase64(string key)
        {
            return new JsonResult(new { key=Base64Utils.Base64String(HexUtils.ToByte(key)) });
        }
        [HttpGet("string")]
        public IActionResult HexToString(string key)
        {
            return new JsonResult(new { key = Encoding.UTF8.GetString(HexUtils.ToByte(key)) });
        }
        [HttpGet("rsaencrypt")]
        public IActionResult RsaEncrypt(string hexKey,string val,string sign)
        {
            string publicXml = "<RSAKeyValue><Modulus>{0}</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            publicXml = string.Format(publicXml, Base64Utils.Base64String(HexUtils.ToByte(hexKey)));
            var res = SecurityUtils.RsaEncrypt(128,val,publicXml,sign);
            //var res = SecurityUtils.RsaEncryptToByte(val, RsaSecurity.RSAPublicKeyJava2DotNet(HexUtils.ToByte(hexKey)));
            return new JsonResult(new { en = res });
        }
    }
}