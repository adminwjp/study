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

namespace Company.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MainController :ControllerBase
    {
        protected readonly IRepository<MainInfo> _repository;
        protected readonly ILogger<MainController> _logger;
        public MainController(IRepository<MainInfo> repository, ILogger<MainController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            var response = ResponseApiUtils.GetResponse(Language.Chinese, Code.QuerySuccess);
            var data = this._repository.Find(it => it.Enable.HasValue&&it.Enable.Value).Include(it=>it.BackgroundImage).Select(it=> (MainInfo)it.Clone()).ToList();
            response.Data = data;
            return new JsonResult(response);
        }

    }
}