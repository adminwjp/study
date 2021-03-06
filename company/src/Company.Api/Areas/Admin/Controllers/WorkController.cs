﻿using System;
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
    [Route("api/v1/admin/[controller]")]
    public class WorkController : BaseController<WorkInfo>
    {
        public WorkController(IRepository<WorkInfo> repository, ILogger<WorkController> logger) : base(repository, logger)
        {
        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] WorkInfo obj)
        {
            obj.Create = true;
            bool suc = false;
            if (Request.Form.Files.Count >0)
            {
                foreach (var file in Request.Form.Files)
                {
                    if (!file.Name.Contains("background_image"))
                    {
                        if (suc)
                        {
                            continue;
                        }
                        else
                        {
                            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
                        }
                    }
                    suc = true;
                    using Stream stream = file.OpenReadStream();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    string suffix = file.FileName.Split('.').LastOrDefault();
                    var name = $"{RandomHelper.Id}.{suffix}";
                    System.IO.File.WriteAllBytes(Core.UploadDirectory + "\\" + Core.UploadWork + "\\" + name, buffer);
                    obj.Image = new ImageInfo()
                    {
                        Name = RandomHelper.Id,
                        Href = $"{RandomHelper.Id}.{suffix}",
                        Src = name,
                        Create = true,
                        Type = Core.Work
                    };
                    if (suc && obj.Category != null)
                    {
                        this.AddMiddleExecet(obj);
                    }
                    var newObj = new WorkInfo() { Category = obj.Category,Image=obj.Image,Create=true };
                    base.Repository.Insert(newObj);
                }
            }
            else
            {
                return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
            }
            if(suc)
            {
                return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.AddSuccess));
            }
            else
            {
                return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
            }
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] WorkInfo obj)
        {
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                if (!file.Name.Contains("background_image"))
                {
                    return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.UploadFileFail));
                }
              
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                string suffix = file.FileName.Split('.').LastOrDefault();
                var name = $"{RandomHelper.Id}.{suffix}";
                System.IO.File.WriteAllBytes(Environment.CurrentDirectory+"\\"+Core.UploadWork + "\\" + name, buffer);
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Image).FirstOrDefault();
                if (obj.Image == null || !obj.Image.Id.HasValue)
                {
                    obj.Image = old.Image;
                }
                obj.CreateDate = old.CreateDate;
                if (obj.Image != null)
                {
                    System.IO.File.Delete(Environment.CurrentDirectory + "\\" + Core.UploadWork + "\\" + obj.Image.Src);
                    obj.Image.Name = RandomHelper.Id;
                    obj.Image.Href = $"{RandomHelper.Id}.{suffix}";
                    obj.Image.Src = name;
                    (((BaseEfRepository<WorkInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Images.Update(obj.Image);
                }
                else
                {
                    obj.Image = new ImageInfo()
                    {
                        Name = RandomHelper.Id,
                        Href = $"{RandomHelper.Id}.{suffix}",
                        Src = name,
                        Type = Core.Work,
                        Create = true
                    };
                    (((BaseEfRepository<WorkInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Images.Add(obj.Image);
                }
            }
            else
            {
                var old = base.Repository.Find(it => it.Id == obj.Id).Include(it => it.Image).FirstOrDefault();
                if (obj.Image == null || !obj.Image.Id.HasValue)
                {
                    obj.Image = old.Image;
                }
                obj.CreateDate = old.CreateDate;
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
            obj.ModifyDate = DateTime.Now;
            base.Repository.Update(obj);
            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.ModifySuccess));
        }
        protected override void AddMiddleExecet(WorkInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = (((BaseEfRepository<WorkInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(WorkInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<WorkInfo> QueryList(WorkInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it=>it.Category).Include(it=>it.Image).Select(it=>(WorkInfo)it.Clone())/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
        [HttpGet("category")]
        public  ResponseApi Category()
        {
            var data= base.Query().Include(it=>it.Image).Select(it=>new WorkInfo() { Id=it.Id,Src=it.Image.Name}).ToList();
            var result = ResponseApi.CreateSuccess();
            result.Data = data;
            return result;
        }
    }
}