using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
    [Route("api/admin/[controller]")]
    public abstract class BaseImgController: BaseController<ImageInfo>
    {
        private readonly ImgFactory _imgFactory;
        public BaseImgController(ImgFactory imgFactory, IRepository<ImageInfo> repository, ILogger<ImgController> logger) : base(repository, logger)
        {
            this._imgFactory = imgFactory;
        }
        //[Consumes("*/*")]
        [HttpGet("get/{id}")]
        public IActionResult Get(string id)
        {
            ImageInfo image = this._imgFactory.Get(id);
            if (image == null)
            {
                return new NotFoundResult();
            }
            else
            {
                string src = string.Empty;
                switch (image.Type)
                {
                    case Core.Work:
                        src = Core.UploadWork;
                        break;
                    case Core.Bg:
                        src = Core.UploadBackgroundImage;
                        break;
                    case Core.Testimonial:
                        src = Core.UploadTestimonial;
                        break;
                    case Core.Brand:
                        src = Core.UploadBrand;
                        break;
                    case Core.Team:
                        src = Core.UploadTeam;
                        break;
                    case Core.Service:
                        src = Core.UploadService;
                        break;
                    default:
                        src = Core.UploadImg;
                        break;
                }
                src = $"{Environment.CurrentDirectory}\\{src}\\{image.Src}";
                if (System.IO.File.Exists(src))
                {
                    if (image.Type == Core.Service)
                    {
                        return PhysicalFile(src, "image/svg+xml");
                    }
                    return File(System.IO.File.ReadAllBytes(src), "*/*");
                    // return new FileContentResult(System.IO.File.ReadAllBytes(src), "*/*");
                }
                return NotFound();

            }
        }
    }
}