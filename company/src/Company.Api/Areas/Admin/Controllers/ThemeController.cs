using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Domain;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.Ef.Repositories;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class ThemeController : BaseController<ThemeInfo>
    {
        protected CompanyDbContext CompanyDbContext { get; set; }
        public ThemeController(IRepository<ThemeInfo> repository, ILogger<ThemeController> logger) : base(repository, logger)
        {
            CompanyDbContext = ((Company.Domain.CompanyDbContext)((BaseEfRepository<ThemeInfo>)base.Repository).DbContext);
        }
        protected override void AddMiddleExecet(ThemeInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                obj.Category = CompanyDbContext.Categories.Find(new object[] { obj.Category.Id });
            }
        }
        protected override void EditMiddleExecet(ThemeInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<ThemeInfo> QueryList(ThemeInfo obj, int? page, int? size)
        {
            return base.Query(base.QueryFilter(null, obj)).Include(it=>it.Category)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
        }
    }
}