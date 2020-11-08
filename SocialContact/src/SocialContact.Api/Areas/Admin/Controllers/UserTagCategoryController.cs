using System.Collections.Generic;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using SocialContact.Api.Models;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Criterion;
using NHibernate.Transform;
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

    public class UserTagCategoryController : SocialContact.Api.Controllers.BaseController<UserTagCategoryInfo, QueryUserTagCategoryFormViewModel, QueryUserTagCategoryInfoResultViewModel>
    {
        public UserTagCategoryController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<UserTagCategoryController> logger) : base(redisCache, unitWork,cache,authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "user_tag_category";
        }
        protected override UserTagCategoryInfo AddMiddlewareExecute(UserTagCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }

        protected override UserTagCategoryInfo EditMiddlewareExecute(UserTagCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserTagCategoryFormViewModel obj)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(obj.Category))
            {
                criterias.Add(Expression.Like("Category",$"%{obj.Category}%"));
                res = true;
            }
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            return res;
        }
        protected override Func<UserTagCategoryInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<UserTagCategoryInfo>();
        }
    }
}