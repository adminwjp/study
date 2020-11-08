using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Areas.Admin.Controllers;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Model;
using Utility.Response;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ImgController : BaseImgController
    {
        public ImgController(ImgFactory imgFactory):base(imgFactory,null,null)
        {
        }
        public override Task<ResponseApi> Add([FromForm] ImageInfo obj)
        {
            throw new NotSupportedException();
        }
        public override Task<ResponseApi> Edit([FromForm] ImageInfo obj)
        {
            throw new NotSupportedException();
        }
        public override Task<ResponseApi> Delete([FromQuery] int? id, [FromBody, FromForm] int[] ids)
        {
            throw new NotSupportedException();
        }
        public override  Task<ResponseApi<ResultModel<ImageInfo>>> Query([FromBody, FromForm] ImageInfo obj, int? page, int? size)
        {
            throw new NotSupportedException();
        }
    }
}