using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Utility.Response;
using Utility.Domain.Repositories;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/basic_category")]
    public class BasicCategoryController : BaseController<BasicCategoryInfo>
    {
        public BasicCategoryController(IRepository<BasicCategoryInfo> repository, ILogger<BasicCategoryController> logger):base(repository,logger)
        {

        }
        protected override List<BasicCategoryInfo> QueryList(BasicCategoryInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
        [HttpGet("category")]
        public virtual async Task<ResponseApi> Category()
        {
            ResponseApi responseApi = ResponseApi.CreateSuccess(GetLanguage());
            var data = base.Repository.Find(null).Select(it => new BasicCategoryInfo()
            {
                Id = it.Id,
                Name = it.Name,
                EnglishName = it.EnglishName
            });
            responseApi.Data = data;
            return await Task.FromResult<ResponseApi>(responseApi);
        }
    }
}