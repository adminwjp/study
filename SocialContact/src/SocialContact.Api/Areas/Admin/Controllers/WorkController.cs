using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using SocialContact.Api.Models;
using NHibernate.Criterion;
using System.Collections.Generic;
using System;
using Utility.Domain.Uow;
using Utility.Redis;
using Utility.Response;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class WorkController : SocialContact.Api.Controllers.BaseController<WorkInfo, QueryWorkFormViewModel, QueryWorkInfoResultViewModel>
    {
        public WorkController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<WorkController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "work";
        }
        protected override WorkInfo EditMiddlewareExecute(WorkInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
            return obj;
        }
        protected override WorkInfo AddMiddlewareExecute(WorkInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryWorkFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (!string.IsNullOrEmpty(obj.CompanyName))
            {
                criterias.Add(Expression.Like("CompanyName", $"%{obj.CompanyName}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.Job))
            {
                criterias.Add(Expression.Like("Job", $"%{obj.Job}%"));
                res = true;
            }
            if (obj.CategoryId !=null&& obj.CategoryId.HasValue)
            {
                criterias.Add((AbstractCriterion)Expression.Where<WorkInfo>(it=>it.Category.Id == obj.CategoryId));
                res = true;
            }
            if (obj.WorkDate != null && obj.WorkDate.Length == 2)
            {
                criterias.Add(Expression.And(Expression.Gt("StartDate", obj.WorkDate[0]), Expression.Lt("EndDate", obj.WorkDate[1])));
                res = true;
            }
            return res;
        }
        protected override Func<WorkInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Job };
        }
    }
}