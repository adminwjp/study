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

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]

    public class UserTagController : SocialContact.Api.Controllers.BaseController<UserTagInfo, QueryUserTagFormViewModel, QueryUserTagInfoResultViewModel>
        {
        public UserTagController(RedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<UserTagController> logger) : base(redisCache, unitWork, cache,authrize, logger)
        {
            base.IsCustomValidator = true;
            base.PageName = "user_tag";
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserTagFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryChildFilterByCategory<UserTagCategoryInfo, QueryUserTagFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        protected override UserTagInfo EditMiddlewareExecute(UserTagInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
            return obj;
        }
        protected override UserTagInfo AddMiddlewareExecute(UserTagInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
            return obj;
        }
        protected override Func<UserTagInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Category.Category };
        }
    }
}