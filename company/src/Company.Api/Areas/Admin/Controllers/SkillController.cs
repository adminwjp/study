using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class SkillController : BaseController<SkillInfo>
    {
        public SkillController(IRepository<SkillInfo> repository, ILogger<SkillController> logger) : base(repository, logger)
        {
        }
        protected override void AddMiddleExecet(SkillInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = ((Company.Domain.CompanyDbContext)base.Repository.DbContext).Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(SkillInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<SkillInfo> QueryList(SkillInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it=>it.Category)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
    }
}