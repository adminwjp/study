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
using Utility.Domain.Repositories;
using Utility.Response;
using Utility.Enums;

namespace Company.Api.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class NavController : ControllerBase
    {
        protected readonly IRepository<NavInfo> _repository;
        protected readonly ILogger<NavController> _logger;
        public NavController(IRepository<NavInfo> repository, ILogger<NavController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            var response = ResponseApi.Create(Language.Chinese, Code.QuerySuccess);
            var data = this._repository.Find(it => it.Enable.HasValue&&it.Enable.Value&&it.Id==it.Parent.Id).Include(it=>it.Children).ToList();
            foreach (var item in data)
            {
                if (item.Children != null)
                {

                    if (item.Children.Count == 1&&item.Id == item.Children.FirstOrDefault().Id)
                    {
                        item.Children = null;
                    }
                    else if (item.Children.Count > 1 && item.Id == item.Children.FirstOrDefault().Id)
                    {
                        List<NavInfo> temp = item.Children.ToList();
                        temp.RemoveAt(0);
                        item.Children = temp;
                    }
                }
            }
            response.Data = data;
            return new JsonResult(response);
        }
    }
}