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
using Microsoft.AspNetCore.Http;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class AboutController : BaseController<AboutInfo>
    {
        public AboutController(IRepository<AboutInfo> repository, ILogger<AboutController> logger) : base(repository, logger)
        {

        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] AboutInfo obj)
        {
            obj.Create = true;
            if (Request.Form.Files.Count == 2)
            {
                var file = Request.Form.Files[0];
                var file1 = Request.Form.Files[1];
                if ((file.Name == "image"&&file1.Name== "background_image") || (file.Name == "background_image" && file1.Name == "image"))
                {
                    if (file.Name == "image" || file.Name == "background_image")
                    {
                        using Stream stream = file.OpenReadStream();
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        string suffix = file.FileName.Split('.').LastOrDefault();
                        var name = $"{RandomUtils.Instance.Id}.{suffix}";
                        System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + name, buffer);
                        var newImg = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Create = true,
                            Type = Core.Image
                        };
                       // (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(newImg);
                        base.Repository.Save();
                        if (file.Name == "image")
                        {
                            obj.Image = newImg;
  
                        }
                        else
                        {
                            obj.BackgroundImage = newImg;
                        }
                   
                    }
                    if (file1.Name == "image" || file1.Name == "background_image")
                    {
                        using Stream stream = file1.OpenReadStream();
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        string suffix = file1.FileName.Split('.').LastOrDefault();
                        var name = $"{RandomUtils.Instance.Id}.{suffix}";
                        System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + name, buffer);
                        var newImg = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Create = true,
                            Type = Core.Image
                        }; 
                        //(base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(newImg);
                        if (file1.Name == "background_image")
                        {
                            obj.BackgroundImage = newImg;
                        }
                        else
                        {
                             obj.Image= newImg;
                        }
                        base.Repository.Save();
                    }

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
        public override async Task<ResponseApi> Edit([FromForm] AboutInfo obj)
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
                    obj = serializer.Deserialize(reader) as AboutInfo;
                }
            }
          
            if (Request.Form.Files.Count == 2)
            {
                var file = Request.Form.Files[0];
                var file1 = Request.Form.Files[1];
                if ((file.Name == "image" && file1.Name == "background_image") || (file.Name == "background_image" && file1.Name == "image"))
                {
                    var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Image).Include(it => it.BackgroundImage).FirstOrDefault();
                    if (file.Name == "image" || file.Name == "background_image")
                    {
                        var result = await Upload(file,obj, old, false);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    if (file1.Name == "image" || file1.Name == "background_image")
                    {
                        var result = await Upload(file1, obj, old, false);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                    obj.ModifyDate = DateTime.Now;
                    base.Repository.Update(obj);
                    base.Repository.Save();
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
                }
                else
                {
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                }
            }
            else  if (Request.Form.Files.Count == 1)
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Img).Include(it => it.BackgroundImage).FirstOrDefault();
                var file = Request.Form.Files[0];
                var result = await Upload(file,obj,old);
                if (result == null)
                {
                    obj.ModifyDate = DateTime.Now;
                    base.Repository.Update(obj);
                    base.Repository.Save();
                    return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
                }
                else
                {
                    return result;
                }
            }
            else if (Request.Form.Files.Count == 0)
            {
                var old = this.Repository.FindSingle(it => it.Id == obj.Id);
                if (obj.Image == null || !obj.Image.Id.HasValue)
                {
                    obj.Image = old.Image;
                }
                else
                {
                    obj.Image = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.Image.Id.Value });
                    if (obj.Image == null)
                    {
                        return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                    }
                }
                if(obj.BackgroundImage==null|| !obj.BackgroundImage.Id.HasValue)
                {
                    obj.BackgroundImage = old.BackgroundImage;
                }
                else
                {
                    obj.BackgroundImage = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.BackgroundImage.Id.Value });
                    if (obj.BackgroundImage == null)
                    {
                        return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                    }
                }
                obj.CreateDate = old.CreateDate;
                obj.ModifyDate = DateTime.Now;
                base.Repository.Update(obj);
                base.Repository.Save();
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
            }
            else
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
            }
        }
        private async Task<ResponseApi> Upload(IFormFile file,AboutInfo obj, AboutInfo old, bool single=true)
        {
            if (file.Name == "image" || file.Name == "background_image")
            {
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomUtils.Instance.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + name, buffer);
               
                if (file.Name == "image")
                {
                    if (obj.Image == null || !obj.Image.Id.HasValue)
                    {
                        obj.Image = old.Image;
                    }
                    else
                    {
                        obj.Image = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.Image.Id.Value });
                        if (obj.Image == null)
                        {
                            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                        }
                    }
                    if (obj.Image == null)
                    {
                        obj.Image = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Create = true,
                            Type = Core.Image
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Image);
                        //base.Repository.Save();
                    }
                    else
                    {
                        System.IO.File.Delete(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + obj.Image.Src);
                        obj.Image.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.Image);
                        base.Repository.Save();
                    }
                    if (single)
                    {
                        if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                        {
                            obj.BackgroundImage = old.BackgroundImage;
                        }
                        else
                        {//dothing 
                            obj.BackgroundImage = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.BackgroundImage.Id.Value });
                            if (obj.BackgroundImage == null)
                            {
                                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                            }
                        }
                    }
                }
                else
                {
                    if (obj.BackgroundImage == null || !obj.BackgroundImage.Id.HasValue)
                    {
                        obj.BackgroundImage = old.BackgroundImage;
                    }
                    else
                    {
                        obj.BackgroundImage = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.BackgroundImage.Id.Value });
                        if (obj.BackgroundImage == null)
                        {
                            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                        }
                    }
                    if (obj.BackgroundImage == null)
                    {
                        obj.BackgroundImage = new ImageInfo()
                        {
                            Name = RandomUtils.Instance.Id,
                            Href = $"{RandomUtils.Instance.Id}.{suffix}",
                            Src = name,
                            Create = true,
                            Type = Core.Image
                        };
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.BackgroundImage);
                        base.Repository.Save();
                    }
                    else
                    {
                        System.IO.File.Delete(Core.UploadDirectory + "\\" + Core.UploadImg + "\\" + obj.BackgroundImage.Src);
                        obj.BackgroundImage.Src = name;
                        (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.BackgroundImage);
                       // base.Repository.Save();
                    }
                    if (single)
                    {
                        if (obj.Image == null || !obj.Image.Id.HasValue)
                        {
                            obj.Image = old.Image;
                        }
                        else
                        {
                            //dothing
                            obj.Image = (base.Repository.DbContext as Company.Domain.CompanyDbContext).Images.Find(new object[] { obj.Image.Id.Value });
                            if (obj.Image == null)
                            {
                                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
                            }
                        }
                    }
                }
                obj.CreateDate = old.CreateDate;
                //obj.ModifyDate = DateTime.Now;
                //base.Repository.Update(obj);
                //base.Repository.Save();
                return null;
            }
            else
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.UploadFileFail));
            }
        }
        protected override List<AboutInfo> QueryList(AboutInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it => it.Image).Include(it=>it.BackgroundImage).Select(it => (AboutInfo)it.Clone())/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
    }
}