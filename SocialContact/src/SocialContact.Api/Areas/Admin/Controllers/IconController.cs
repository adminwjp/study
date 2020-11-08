using System.Linq;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;
using NHibernate.Criterion;
using Utility;
using SocialContact.Api.Models;
using Utility.Enums;
using Utility.Response;
using Utility.Domain.Uow;
using Utility.Redis;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class IconController : SocialContact.Api.Controllers.BaseController<IconInfo, QueryIconFormViewModel, QueryIconInfoResultViewModel>
    {
        public IconController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<IconController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "icon";
        }
        protected override IconInfo AddMiddlewareExecute(IconInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            SetAdmin(obj);
            return obj;
        }

        protected override IconInfo EditMiddlewareExecute(IconInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryIconFormViewModel obj)
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
        [HttpGet("category")]
        public override async Task<ResponseApi> Category()
        {
            List<IconViewModel> iconViewModels = UnitWork.Find<IconInfo>(null).Select(it => new IconViewModel() { Id = it.Id.Value, Value = it.Style, Label = it.Name }).ToList();
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = iconViewModels;
            return await Task.FromResult(response);
        }
    }
}