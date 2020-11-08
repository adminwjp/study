using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.Enums;
using Utility.Response;

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        protected readonly IRepository<MediaInfo> _repository;
        protected readonly ILogger<MediaController> _logger;
        public MediaController(IRepository<MediaInfo> repository, ILogger<MediaController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            var response = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            var data = this._repository.Find(it => it.Enable.HasValue && it.Enable.Value).ToList();
            response.Data = data;
            return new JsonResult(response);
        }
    }
}