using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Company.Domain;
using System.IO;
using Company.Api.Data;
using System.Xml.Serialization;

namespace Company.Api.Areas.Admin.Controllers
{

    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class BrandController : BaseController<BrandInfo>
    {
        public BrandController(IRepository<BrandInfo> repository, ILogger<BrandController> logger):base(repository,logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] BrandInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "logo")
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomUtils.Instance.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadBrand + "\\" + name, buffer);
                obj.Logo = new ImageInfo()
                {
                    Name = RandomUtils.Instance.Id,
                    Href = $"{RandomUtils.Instance.Id}.{suffix}",
                    Src = name,
                    Create = true,
                    Type = Core.Brand
                };
                base.Repository.Save();
            }
            else
            {
                if(string.IsNullOrWhiteSpace(obj.Feature))
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
            }
            this.AddMiddleExecet(obj);
            obj.CreateDate = DateTime.Now;
            base.Repository.Add(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] BrandInfo obj)
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
                    obj = serializer.Deserialize(reader) as BrandInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "logo")
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomUtils.Instance.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadBrand + "\\" + name, buffer);
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Logo).FirstOrDefault();
                if (obj.Logo == null || !obj.Logo.Id.HasValue)
                {
                    obj.Logo = old.Logo;
                }
                obj.CreateDate = old.CreateDate;
                if (obj.Logo != null)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadBrand + "\\" + obj.Logo.Src);
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
                        Type = Core.Brand,
                        Create = true
                    };
                    (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Logo);
                }
                base.Repository.Save();
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Logo).FirstOrDefault();
                if (obj.Logo == null || !obj.Logo.Id.HasValue)
                {
                    obj.Logo = old.Logo;
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
        protected override void AddMiddleExecet(BrandInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = ((Company.Domain.CompanyDbContext)base.Repository.DbContext).Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(BrandInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<BrandInfo> QueryList(BrandInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj)).Include(it => it.Logo).Include(it=>it.Category).Select(it=> (BrandInfo)it.Clone())/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            //var context = (base.Repository.DbContext as Company.Domain.CompanyDbContext);
            //var where = QueryFilter(null, obj);
            //var result = (where == null ? context.Brands: context.Brands.Where(QueryFilter(null, obj))).Join(context.Images,it=>it.Logo.Id,it=>it.Id,(it,it1)=>new {it,it1 })
            //    .Join(context.Categories,it=>it.it.Category.Id,it=>it.Id,(it,it1)=>(BrandInfo)it.it.Clone(it.it1,it1))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
    }
}