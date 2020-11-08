using System.Collections.Generic;
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
using SocialContact.Api.Models;
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

    public class JobCategoryController : SocialContact.Api.Controllers.BaseController<JobCategoryInfo, QueryJobCategoryFormViewModel, QueryJobCategoryInfoResultViewModel>
    {
        public JobCategoryController(IRedisCache redisCache,IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<JobCategoryController> logger) : base(redisCache,unitWork,cache,authrize,  logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "job_category";
        }
        protected override JobCategoryInfo EditMiddlewareExecute(JobCategoryInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override JobCategoryInfo AddMiddlewareExecute(JobCategoryInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryJobCategoryFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (base.QueryFilterByCategory<EdutionCategoryInfo, QueryJobCategoryFormViewModel>(ref criterias, obj)) res = true;
            return res;
        }
        /// <summary>
        /// 实现树形列表查询 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override IQueryable<JobCategoryInfo> QueryChildFilter(IQueryable<JobCategoryInfo> query, QueryJobCategoryFormViewModel obj)
        {
           // query = base.QueryChildFilter(query);
            return query;
        }
        [HttpGet("parent_category")]

        public async Task<ResponseApi> QueryCategory()
        {
            List<JobCategoryInfo> jobCategoryInfos = UnitWork.Find<JobCategoryInfo>(null).ToList();
            jobCategoryInfos = this.DataParseIfWhileReference(jobCategoryInfos);
            List<CategoryViewModel>  categoryViewModels = ObjectMapper.Map<List<CategoryViewModel>>(jobCategoryInfos);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = categoryViewModels;
            return await Task.FromResult(response);
        }
        protected override Func<JobCategoryInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<JobCategoryInfo>();
        }
        protected override List<JobCategoryInfo> DataParseIfWhileReference(List<JobCategoryInfo> data, bool where = false)
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