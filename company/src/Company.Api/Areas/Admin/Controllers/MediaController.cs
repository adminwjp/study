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

namespace Company.Api.Areas.Admin.Controllers
{

    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class MediaController : BaseController<MediaInfo>
    {
        public MediaController(IRepository<MediaInfo> repository, ILogger<MediaController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(MediaInfo obj)
        {
            
        }
        protected override void EditMiddleExecet(MediaInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<MediaInfo> QueryList(MediaInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
    }
}