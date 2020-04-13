using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class RecordController : BaseController<RecordInfo>
    {
        public RecordController(ILogger<RecordController> logger, IRepository<RecordInfo> repository) : base(logger, repository)
        {

        }
        [HttpPost("add")]
        public override ResponseApi Add([FromForm] RecordInfo obj)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref obj, reader.ReadToEnd());
                    Ref(ref obj, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(RecordInfo);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as RecordInfo;
            }
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApiUtils.Fail();
            //}
            if (Request.Form.Files.Count != 1)
            {
                return ResponseApiUtils.GetResponse( Language.Chinese,Code.UploadFileFail);
            }
            var file = Request.Form.Files[0];
            if (file.Name.ToLower() != "photo")
            {
                return ResponseApiUtils.GetResponse(Language.Chinese, Code.UploadFileFail);
            }
            using Stream stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer,0,buffer.Length);
            FileUtils.WriteFile(Program.UploadImg + "\\" + file.FileName, buffer);
            obj.Photo = "imgs\\" + file.FileName;
            obj.CreateDate = DateTime.Now;
            this.Repository.Add(obj);
            return ResponseApiUtils.Success();
        }
        [HttpPost("edit")]
        public override ResponseApi Edit([FromForm] RecordInfo obj)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref obj, reader.ReadToEnd());
                    Ref(ref obj, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(RecordInfo);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as RecordInfo;
            }
            if (Request.Form.Files.Count != 1)
            {
                return ResponseApiUtils.GetResponse(Language.Chinese, Code.UploadFileFail);
            }
            var file = Request.Form.Files[0];
            if (file.Name.ToLower() != "photo")
            {
                return ResponseApiUtils.GetResponse(Language.Chinese, Code.UploadFileFail);
            }
            using Stream stream = file.OpenReadStream();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            FileUtils.WriteFile(Program.UploadImg + "\\" + file.FileName, buffer);
            obj.Photo = "imgs\\" + file.FileName;
            obj.UpdateDate = DateTime.Now;
            this.Repository.Update(it=>it.Id==obj.Id,it=>obj);
            return ResponseApiUtils.Success();
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApiUtils.Success().SetData(data);
        }
		//批准人
		[HttpGet("ratifier")]
        public ResponseApi Ratifier()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApiUtils.Success().SetData(data);
        }
    }
}