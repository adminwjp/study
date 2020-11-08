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
using Utility.Ef.Repositories;
using Utility.Response;

namespace Company.Api.Areas.Admin.Controllers
{

    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/work_category")]
    public class WorkCategoryController : BaseController<WorkCategoryInfo>
    {
        public WorkCategoryController(IRepository<WorkCategoryInfo> repository, ILogger<WorkCategoryController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(WorkCategoryInfo obj)
        {
            if (obj.WorkId.HasValue)
            {
                obj.Work = (((BaseEfRepository<WorkCategoryInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Works.Find(new object[] { obj.WorkId });
            }
            else
            {
                obj.Work = null;//无法清空引用 外键信息只能手动 清除
            }
            //System.InvalidOperationException: The instance of entity type
            if (obj.ParentId.HasValue&& obj.ParentId != obj.Id)
            {
                
                obj.Parent = (((BaseEfRepository<WorkCategoryInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).WorkCategories.Find(new object[] { obj.ParentId });
            }
            else
            {
                obj.Parent = obj;
            }
        }
        protected override void EditMiddleExecet(WorkCategoryInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<WorkCategoryInfo> QueryList(WorkCategoryInfo obj, int? page, int? size)
        {
            var result = base.Query(QueryFilter(null, obj)).Include(it=>it.Work)/*.Include(it=>it.Children)*/.Include(it=>it.Parent)

                .Select(it => new WorkCategoryInfo()
                {
                    Id = it.Id,
                    Name = it.Name,
                    EnglishName = it.EnglishName,
                    CreateDate = it.CreateDate,
                    ModifyDate = it.ModifyDate,
                    Enable = it.Enable,
                    Filter=it.Filter,
                    WorkId=it.Work.Id,
                    ParentId= it.Parent!=null?it.Parent.Id:null
                })

            /*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return result;
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Query(it=>!it.Work.Id.HasValue).Select(it => new WorkCategoryInfo()
            {
                Id = it.Id,
                Name = it.Name,
                EnglishName = it.EnglishName
            });
            var result = ResponseApi.CreateSuccess();
            result.Data = data;
            return result;
        }
    }
}