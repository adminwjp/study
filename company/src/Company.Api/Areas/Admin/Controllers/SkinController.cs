using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class SkinController : BaseController<SkinInfo>
    {
        public SkinController(IRepository<SkinInfo> repository, ILogger<SkinController> logger):base(repository,logger)
        {

        }
        protected override List<SkinInfo> QueryList(SkinInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null,obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
        [HttpGet("get")]
        public virtual async Task<Utility.ResponseApi> Get()
        {
            Utility.ResponseApi ResponseApi = ResponseApiUtils.Success(GetLanguage());
            ResponseApi.Data = this.Query().ToList();
            return await Task.FromResult<Utility.ResponseApi>(ResponseApi);
        }
    }
}