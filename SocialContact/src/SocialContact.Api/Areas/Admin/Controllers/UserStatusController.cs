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
using Utility.Redis;
using Utility.Domain.Uow;
using Utility.Response;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class UserStatusController : SocialContact.Api.Controllers.BaseController<UserStatusInfo, QueryUserStatusFormViewModel, QueryUserStatusInfoResultViewModel>
    {
        public UserStatusController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<UserStatusController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "user_status";
        }
        protected override UserStatusInfo AddMiddlewareExecute(UserStatusInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }

        protected override UserStatusInfo EditMiddlewareExecute(UserStatusInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserStatusFormViewModel obj)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(obj.Name))
            {
                criterias.Add(Expression.Like("Name", $"%{obj.Name}%"));
                res = true;
            }
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            return res;
        }
        protected override Func<UserStatusInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Name };
        }
    }
}