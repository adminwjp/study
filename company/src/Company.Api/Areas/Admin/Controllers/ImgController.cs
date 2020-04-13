using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class ImgController : BaseImgController
    {
        public ImgController(ImgFactory imgFactory, IRepository<ImageInfo> repository, ILogger<ImgController> logger) : base(imgFactory,repository, logger)
        {
        }
        //[Consumes("*/*")] 
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([FromForm] ImageInfo obj)
        {
            if (Request.Form.Files.Count ==1)
            {
                var file = Request.Form.Files[0];
                var suffix = file.Name.Split('.').LastOrDefault();
                obj.Name = RandomUtils.Instance.Id;
                obj.Src = $"{RandomUtils.Instance.Id}.{suffix}";
                obj.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer,0,buffer.Length);
                System.IO.File.WriteAllBytes($"{Environment.CurrentDirectory}\\{Core.UploadImg}\\{obj.Src}", buffer);
            }
            else
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(Language.Chinese, Code.UploadFileFail));
            }
            return await base.Add(obj);
        }
        [HttpPost("edit")]
        public override async Task<ResponseApi> Edit([FromForm] ImageInfo obj)
        {
            if (Request.Form.Files.Count == 1)
            {
                var file = Request.Form.Files[0];
                var suffix = file.Name.Split('.').LastOrDefault();
                obj.Name = RandomUtils.Instance.Id;
                obj.Src = $"{RandomUtils.Instance.Id}.{suffix}";
                obj.Href = $"{RandomUtils.Instance.Id}.{suffix}";
                using Stream stream = file.OpenReadStream();
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                System.IO.File.WriteAllBytes($"{Environment.CurrentDirectory}\\{Core.UploadImg}\\{obj.Src}", buffer);
            }
            else
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(Language.Chinese, Code.UploadFileFail));
            }
            return await base.Edit(obj);
        }
        protected override List<ImageInfo> QueryList(ImageInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
    }
}