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
    public class NavController : BaseController<NavInfo>
    {
        public NavController(IRepository<NavInfo> repository, ILogger<NavController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(NavInfo obj)
        {
            if (obj.Parent != null && obj.Parent.Id.HasValue)
            {
                if (obj.Id.HasValue&&obj.Id==obj.Parent.Id)
                {
                    obj.Parent = obj;
                }
                else
                {
                    obj.Parent = (base.Repository.DbContext as Company.Domain.CompanyDbContext).NavInfos.Find(new object[] { obj.Parent.Id });
                }
            }
            else if (obj.ParentId.HasValue)
            {
                if (obj.Id.HasValue && obj.Id == obj.ParentId)
                {
                    obj.Parent = obj;
                }
                else
                {
                    obj.Parent = (base.Repository.DbContext as Company.Domain.CompanyDbContext).NavInfos.Find(new object[] { obj.ParentId });
                }
            }
            else
            {
               // obj.Parent = obj;
            }
        }
        protected override void EditMiddleExecet(NavInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<NavInfo> QueryList(NavInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj)).Include(it=>it.Parent)

                .Select(it => new NavInfo()
                {
                    Id = it.Id,
                    Name = it.Name,
                    EnglishName = it.EnglishName,
                    Href = it.Href,
                    CreateDate = it.CreateDate,
                    ModifyDate = it.ModifyDate,
                    Enable = it.Enable,
                    ParentId = it.Parent != null ? it.Parent.Id : null
                })

            /*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Query(it => it.Parent.Id==it.Id||it.Parent==null).Select(it => new NavInfo()
            {
                Id=it.Id,
                Name=it.Name,
                EnglishName=it.EnglishName
            });
            var result = ResponseApiUtils.Success();
            result.Data = data;
            return result;
        }
    }
}