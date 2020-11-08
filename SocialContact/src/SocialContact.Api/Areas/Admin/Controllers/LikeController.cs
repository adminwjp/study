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
using System.Collections.Generic;
using NHibernate.Criterion;
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

    public class LikeController : SocialContact.Api.Controllers.BaseController<LikeInfo, QueryLikeFormViewModel, QueryLikeInfoResultViewModel>
    {
        public LikeController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<LikeController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "like";
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryLikeFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryChildFilterByCategory<LikeCategoryInfo, QueryLikeFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        protected override LikeInfo EditMiddlewareExecute(LikeInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            this.Set(obj);
            return obj;
        }
        protected override LikeInfo AddMiddlewareExecute(LikeInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            this.Set(obj);
            return obj;
        }
        void Set(LikeInfo obj)
        {
            obj.User = obj.User == null || !obj.User.Id.HasValue ? null : obj.User;
        }
        protected override Func<LikeInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Category.Category };
        }
    }
}