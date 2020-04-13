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

namespace Company.Api.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        protected readonly IRepository<AboutInfo> _repository;
        protected readonly ILogger<AboutController> _logger;
        public AboutController(IRepository<AboutInfo> repository, ILogger<AboutController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            var response = ResponseApiUtils.GetResponse(Language.Chinese, Code.QuerySuccess);
            var data = this._repository.Find(it => it.Enable.HasValue&&it.Enable.Value).Include(it=>it.BackgroundImage).Include(it=>it.Image).Select(it=> (AboutInfo)it.Clone()).FirstOrDefault();
            response.Data = data;
            return new JsonResult(response);
        }
    }
}