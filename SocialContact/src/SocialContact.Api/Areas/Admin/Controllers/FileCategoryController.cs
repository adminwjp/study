using System.Linq;
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
using SocialContact.Api.Data;
using SocialContact.Api.Models;
using System.Collections.Generic;
using Utility.Response;
using Utility.Redis;
using Utility.Domain.Uow;
using Utility.Enums;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class FileCategoryController : SocialContact.Api.Controllers.BaseController<FileCategoryInfo, QueryFileCategoryFormViewModel, QueryFileCategoryInfoResultViewModel>
    {
        public FileCategoryController(IRedisCache redisCache, IObjectMapper objectMapper, IUnitWork unitWork,IMemoryCache cache, AuthrizeValidator authrize, ILogger<FileCategoryController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "file_category";
        }
        protected override FileCategoryInfo AddMiddlewareExecute(FileCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryFileCategoryFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if(base.QueryFilterByCategory<FileCategoryInfo,QueryFileCategoryFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        protected override FileCategoryInfo EditMiddlewareExecute(FileCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            return obj;
        }
        [HttpGet("category")]
        public override async Task<ResponseApi> Category()
        {
            using (NHibernate.ISession session = HttpContext.RequestServices.GetService<NHibernate.ISession>())
            {
                var result = session.CreateCriteria<FileCategoryInfo>().SetProjection(new IProjection[] { Projections.Property("Id").As("Id"),
                    Projections.Property("Category").As("Category"),  Projections.Property("Accept").As("Accept") })
                    .SetResultTransformer(new AliasToBeanResultTransformer(typeof(FileCategoryEntry))).List<FileCategoryEntry>();
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
                response.Data = result;
                return await Task.FromResult(response);
            }
        }
        [HttpGet("getdata")]
        public ResponseApi GetData()
        {
            return Test ? new ResponseApi()
            {
                Message = "≤È—Ø≥…π¶!",
                Success = true,
                Code = 20000,
                Data = base.Cache.Get(Core.FileCategoryChannel)
            } : null;
        }
    }
}