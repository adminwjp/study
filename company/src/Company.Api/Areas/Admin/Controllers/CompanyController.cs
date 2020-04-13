using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Diagnostics.CodeAnalysis;
using Company.Domain;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Company.Api.Data;
using System;
using System.Xml.Serialization;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class CompanyController : BaseController<CompanyInfo>
    {
        public CompanyController(IRepository<CompanyInfo> repository, ILogger<CompanyController> logger) : base(repository, logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] CompanyInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "logo")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + name, buffer);
                    obj.Logo = new ImageInfo()
                    {
                        Name = RandomUtils.Instance.Id,
                        Href = $"{RandomUtils.Instance.Id}.{suffix}",
                        Src = name,
                        Create = true,
                        Type = Core.Image
                    };
                    base.Repository.Save();
                }
                else
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
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
        public override async Task<ResponseApi> Edit([FromForm] CompanyInfo obj)
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
                    obj = serializer.Deserialize(reader) as CompanyInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "logo")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadImg + "\\" + name, buffer);
                    if (obj.Id.HasValue)
                    {
                        var old = base.Repository.Find(it => it.Id == obj.Id).Include(it=>it.Logo).FirstOrDefault();
                        if (obj.Logo == null || !obj.Logo.Id.HasValue)
                        {
                            obj.Logo = old.Logo;
                        }
                        else
                        {//dothing
                            obj.Logo = null;
                        }
                        obj.CreateDate = old.CreateDate;
                    }
                    if (obj.Logo != null)
                    {
                        System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadImg + "\\" + obj.Logo.Src);
                        obj.Logo.Name = RandomUtils.Instance.Id;
                        obj.Logo.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                        obj.Logo.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.Logo);
                    }
                    else
                    {
                        obj.Logo = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Type = Core.Image,
                            Create = true
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Logo);
                    }
                    base.Repository.Save();
                }
                else
                {
                    if (!obj.Id.HasValue)
                    {
                        return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                    }
                }
            }
            else
            {
                if (!obj.Id.HasValue)
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Logo).FirstOrDefault();
                if (obj.Logo == null || !obj.Logo.Id.HasValue)
                {
                    obj.Logo = old.Logo;
                }
                obj.CreateDate = old.CreateDate;
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
            if (obj.Id.HasValue)
            {
                obj.ModifyDate = DateTime.Now;
                base.Repository.Update(obj);
                base.Repository.Save();
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
            }
            else
            {
                base.Repository.Add(obj);
                base.Repository.Save();
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
            }
        }

        protected override List<CompanyInfo> QueryList(CompanyInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it => it.Logo).Select(it => new CompanyInfo()
            {
                Id = it.Id,
                Name = it.Name,
                EnglishName = it.EnglishName,
                Description = it.Description,
                EnglishDescription = it.EnglishDescription,
                CreateDate = it.CreateDate,
                ModifyDate = it.ModifyDate,
                Enable = it.Enable,
                Src = it.Logo.Name
            })/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
    }
}