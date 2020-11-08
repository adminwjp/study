using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Api.Data;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.Response;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class SocialController : BaseController<SocialInfo>
    {
        public SocialController(IRepository<SocialInfo> repository, ILogger<SocialController> logger) : base(repository, logger)
        {
        }
        protected override List<SocialInfo> QueryList(SocialInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
        [HttpGet("category")]
        public virtual async Task<ResponseApi> Category()
        {
            ResponseApi responseApi = ResponseApi.CreateSuccess(GetLanguage());
            var data = base.Repository.Find(it=>it.Enable.HasValue&&it.Enable.Value).Select(it => new { Icon= it.Icon ,Id=it.Id});
            responseApi.Data = data;
            return await Task.FromResult<ResponseApi>(responseApi);
        }
    }
}