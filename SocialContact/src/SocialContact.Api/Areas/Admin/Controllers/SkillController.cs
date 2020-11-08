using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using SocialContact.Api.Models;
using NHibernate.Criterion;
using System.Collections.Generic;
using NHibernate.Linq;
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

    public class SkillController : SocialContact.Api.Controllers.BaseController<SkillInfo, QuerySkillFormViewModel, QuerySkillInfoResultViewModel>
    {
        public SkillController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<SkillController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "skill";
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QuerySkillFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryChildFilterByCategory<SkillCategoryInfo, QuerySkillFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        protected override SkillInfo EditMiddlewareExecute(SkillInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            this.Set(obj);
            return obj;
        }
        protected override SkillInfo AddMiddlewareExecute(SkillInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            this.Set(obj);
            return obj;
        }
        void Set(SkillInfo obj)
        {
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
        }
        protected override Func<SkillInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Category.Category };
        }
    }
}