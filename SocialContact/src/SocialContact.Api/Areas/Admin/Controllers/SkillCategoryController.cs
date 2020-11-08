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
using System;
using Utility.Response;
using Utility.Domain.Uow;
using Utility.Redis;
using Utility.ObjectMapping;
using Utility.Enums;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class SkillCategoryController : SocialContact.Api.Controllers.BaseController<SkillCategoryInfo, QuerySkillCategoryFormViewModel, QuerySkillCategoryInfoResultViewModel>
    {
        public SkillCategoryController(IRedisCache redisCache,IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<SkillCategoryController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "skill_category";
        }
        protected override SkillCategoryInfo EditMiddlewareExecute(SkillCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override SkillCategoryInfo AddMiddlewareExecute(SkillCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QuerySkillCategoryFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryFilterByCategory<SkillCategoryInfo, QuerySkillCategoryFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        /// <summary>
        /// 实现树形列表查询 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override IQueryable<SkillCategoryInfo> QueryChildFilter(IQueryable<SkillCategoryInfo> query, QuerySkillCategoryFormViewModel obj)
        {
            // query = base.QueryChildFilter(query);
            return query;
        }
        [HttpGet("parent_category")]

        public async Task<ResponseApi> QueryCategory()
        {
            List<SkillCategoryInfo> skillCategoryInfos = UnitWork.Find<SkillCategoryInfo>(null).ToList();
            skillCategoryInfos = this.DataParseIfWhileReference(skillCategoryInfos);
            List<CategoryViewModel> categoryViewModels = ObjectMapper.Map<List<CategoryViewModel>>(skillCategoryInfos);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = categoryViewModels;
            return await Task.FromResult(response);
        }
        protected override Func<SkillCategoryInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<SkillCategoryInfo>();
        }
        protected override List<SkillCategoryInfo> DataParseIfWhileReference(List<SkillCategoryInfo> data, bool where = false)
        {
            if (data.Any())
            {
                if (where)
                {
                    return base.RecursionDataParseByWhere(data);
                }
                else
                {
                    return base.RecursionDataParse(data, null);
                }
            }
            else
            {
                return data;
            }
        }
    }
}