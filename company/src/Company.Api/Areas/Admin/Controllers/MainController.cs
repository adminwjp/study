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
    public class MainController : BaseController<MainInfo>
    {
        public MainController(IRepository<MainInfo> repository, ILogger<MainController> logger) : base(repository, logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] MainInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "background_image")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadBackgroundImage + "\\" + name, buffer);
                    obj.BackgroundImage = new ImageInfo()
                    {
                        Name = RandomUtils.Instance.Id,
                        Href = $"{RandomUtils.Instance.Id}.{suffix}",
                        Src = name,
                        Create = true,
                        Type = Core.Bg
                    };
                    base.Repository.Save();
                }
            }
            this.AddMiddleExecet(obj);
            obj.CreateDate = DateTime.Now;
            base.Repository.Add(obj);
            base.Repository.Save();
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] MainInfo obj)
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
                    obj = serializer.Deserialize(reader) as MainInfo;
                }
            }
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (file.Name == "background_image")
                {
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomUtils.Instance.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Environment.CurrentDirectory + "\\" + Core.UploadBackgroundImage + "\\" + name, buffer);
                    var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.BackgroundImage).FirstOrDefault();
                    if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                    {
                        obj.BackgroundImage = old.BackgroundImage;
                    }
                    obj.CreateDate = old.CreateDate;
                    if (obj.BackgroundImage != null)
                    {
                        System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadBackgroundImage + "\\" + obj.BackgroundImage.Src);
                        obj.BackgroundImage.Name = RandomUtils.Instance.Id;
                        obj.BackgroundImage.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                        obj.BackgroundImage.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.BackgroundImage);
                    }
                    else
                    {
                        obj.BackgroundImage = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Type = Core.Bg,
                            Create = true
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.BackgroundImage);
                    }
                    base.Repository.Save();
                }
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.BackgroundImage).FirstOrDefault();
                if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                {
                    obj.BackgroundImage = old.BackgroundImage;
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
        protected override void AddMiddleExecet(MainInfo obj)
        {
            
        }
        protected override void EditMiddleExecet(MainInfo obj)
        {
        
        }
        protected override List<MainInfo> QueryList(MainInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it=>it.BackgroundImage).Select(it=>(MainInfo)it.Clone())/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
       
    }
}