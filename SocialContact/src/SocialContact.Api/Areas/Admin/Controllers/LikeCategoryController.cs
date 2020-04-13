using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using NHibernate.Criterion;
using System.Collections.Generic;
using SocialContact.Api.Models;
using System;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]

    public class LikeCategoryController : SocialContact.Api.Controllers.BaseController<LikeCategoryInfo, QueryLikeCategoryFormViewModel, QueryLikeCategoryInfoResultViewModel>
    {
        public LikeCategoryController(RedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<LikeCategoryController> logger) : base(redisCache, unitWork,cache,authrize, logger)
        {
            base.IsCustomValidator = true;
            base.PageName = "like_category";
        }
        protected override LikeCategoryInfo EditMiddlewareExecute(LikeCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override LikeCategoryInfo AddMiddlewareExecute(LikeCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryLikeCategoryFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryFilterByCategory<LikeCategoryInfo, QueryLikeCategoryFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        /// <summary>
        /// 实现树形列表查询 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override IQueryable<LikeCategoryInfo> QueryChildFilter(IQueryable<LikeCategoryInfo> query, QueryLikeCategoryFormViewModel obj)
        {
            // query = base.QueryChildFilter(query);
            return query;
        }
        [HttpGet("parent_category")]

        public async Task<Utility.ResponseApi> QueryCategory()
        {
            List<LikeCategoryInfo> likeCategoryInfos = UnitWork.Find<LikeCategoryInfo>(null).ToList();
            likeCategoryInfos = this.DataParseIfWhileReference(likeCategoryInfos);
            List<CategoryViewModel> categoryViewModels = AutoMapperUtils.MapTo<List<CategoryViewModel>>(likeCategoryInfos);
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.QuerySuccess);
            response.Data = categoryViewModels;
            return await Task.FromResult(response);
        }
        protected override Func<LikeCategoryInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<LikeCategoryInfo>();
        }
        protected override List<LikeCategoryInfo> DataParseIfWhileReference(List<LikeCategoryInfo> data, bool where = false)
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