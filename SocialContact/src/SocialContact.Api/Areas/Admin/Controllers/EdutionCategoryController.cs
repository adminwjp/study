using System.Collections.Generic;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Criterion;
using NHibernate.Transform;
using SocialContact.Api.Models;
using System;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]

    public class EdutionCategoryController : SocialContact.Api.Controllers.BaseController<EdutionCategoryInfo, QueryEdutionCategoryFormViewModel, QueryEdutionCategoryInfoResultViewModel>
    {
        public EdutionCategoryController(RedisCache redisCache, IUnitWork unitWork,IMemoryCache cache, AuthrizeValidator authrize, ILogger<EdutionCategoryController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.IsCustomValidator = true;
            base.PageName = "edution_category";
        }
        protected override EdutionCategoryInfo AddMiddlewareExecute(EdutionCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        protected override EdutionCategoryInfo EditMiddlewareExecute(EdutionCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryEdutionCategoryFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryFilterByCategory<EdutionCategoryInfo, QueryEdutionCategoryFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        protected override Func<EdutionCategoryInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<EdutionCategoryInfo>();
        }
    }
}