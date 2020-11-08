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
using Utility.Domain.Repositories;
using Utility.Ef.Repositories;
using Utility.Enums;
using Utility.Randoms;
using Utility.Response;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/testimonial_person")]
    public class TestimonialPersonController : BaseController<TestimonialPersonInfo>
    {
        public TestimonialPersonController(IRepository<TestimonialPersonInfo> repository, ILogger<TestimonialPersonController> logger) : base(repository, logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] TestimonialPersonInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "person_pic")
                {
                    return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
                }
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomHelper.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadTestimonial + "\\" + name, buffer);
                obj.PersonPic = new ImageInfo()
                {
                    Name = RandomHelper.Id,
                    Href = $"{RandomHelper.Id}.{suffix}",
                    Src = name,
                    Create = true,
                    Type = Core.Testimonial
                };
            }
            else
            {
                return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
            }
            this.AddMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);//作用域 不同 没用
            obj.CreateDate = DateTime.Now;
            this.Repository.Insert(obj);
            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.AddSuccess));
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] TestimonialPersonInfo obj)
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
                    obj = serializer.Deserialize(reader) as TestimonialPersonInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name != "person_pic")
                {
                    return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
                }
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomHelper.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadTestimonial + "\\" + name, buffer);
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.PersonPic).FirstOrDefault();
                if (obj.PersonPic == null || !obj.PersonPic.Id.HasValue)
                {
                    obj.PersonPic = old.PersonPic;
                }
                obj.CreateDate = old.CreateDate;
                if (obj.PersonPic != null)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadTestimonial + "\\" + obj.PersonPic.Src);
                    obj.PersonPic.Name = RandomHelper.Id;
                    obj.PersonPic.Href = $"{RandomHelper.Id}.{suffix}";
                    obj.PersonPic.Src = name;
                    (((BaseEfRepository<TestimonialPersonInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.PersonPic);
                }
                else
                {
                    obj.PersonPic = new ImageInfo()
                    {
                        Name = RandomHelper.Id,
                        Href = $"{RandomHelper.Id}.{suffix}",
                        Src = name,
                        Type = Core.Testimonial,
                        Create = true
                    };
                    (((BaseEfRepository<TestimonialPersonInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.PersonPic);
                }
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.PersonPic).FirstOrDefault();
                if (obj.PersonPic == null || !obj.PersonPic.Id.HasValue)
                {
                    obj.PersonPic = old.PersonPic;
                }
                obj.CreateDate = old.CreateDate;
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
          
            obj.ModifyDate = DateTime.Now;
            this.Repository.Update(obj);
            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.ModifySuccess));
        }
        protected override void AddMiddleExecet(TestimonialPersonInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = (((BaseEfRepository<TestimonialPersonInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(TestimonialPersonInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<TestimonialPersonInfo> QueryList(TestimonialPersonInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj)).Include(it => it.Category).Include(it => it.PersonPic)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
    }
}