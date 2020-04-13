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

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]

    public class RoleController : SocialContact.Api.Controllers.BaseController<AdminRoleInfo,QueryRoleFormViewModel,QueryRoleInfoResultViewModel>
    {
        public RoleController(RedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<RoleController> logger):base(redisCache,unitWork,cache,authrize,logger)
        {
            base.IsCustomValidator = true;
            base.PageName = "role";
        }
        [HttpGet("parent_category")]

        public async Task<Utility.ResponseApi> QueryCategory()
        {
            List<AdminRoleInfo> roleInfos = UnitWork.Find<AdminRoleInfo>(it => it.Id == it.Parent.Id||!it.Id.HasValue).ToList();
            roleInfos = this.DataParseIfWhileReference(roleInfos);
            List<RoleCategoryViewModel> roleCategories = AutoMapperUtils.MapTo<List<RoleCategoryViewModel>>(roleInfos);
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.QuerySuccess);
            response.Data = roleCategories;
            return await Task.FromResult(response);
        }
        protected override Func<AdminRoleInfo, CategoryEntry> Select()
        {
            return base.SelectByCategory<AdminRoleInfo>();
        }
        protected override Task<Dictionary<string, List<string>>> CustomValidate(Dictionary<string, List<string>> errors, AdminRoleInfo obj,int way=0)
        {
            if (!base.Test&& base.AuthrizeValidator.RoleRequired(GetUserId()))
            {
                if (obj.Parent == null || !obj.Parent.Id.HasValue)
                {
                    errors.Add("parent_id",new List<string>() { "请选择角色分类"});
                }
            }
            return base.CustomValidate(errors, obj,way);
        }
        [HttpGet("required")]

        public async Task<Utility.ResponseApi> Required()
        {
            Utility.ResponseApi response = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.QuerySuccess);
            response.Data  = base.Test ? false : base.AuthrizeValidator.RoleRequired(GetUserId());
            return await Task.FromResult(response);
        }
        protected override AdminRoleInfo EditMiddlewareExecute(AdminRoleInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        protected override AdminRoleInfo AddMiddlewareExecute(AdminRoleInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            base.SetParent(obj);
            return obj;
        }
        /// <summary>
        /// 实现树形列表查询 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected override IQueryable<AdminRoleInfo> QueryChildFilter(IQueryable<AdminRoleInfo> query, QueryRoleFormViewModel obj)
        {
            query = base.QueryChildFilter(query);
            return query;
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryRoleFormViewModel obj)
        {
            bool res = false;
            if (obj.ParentId.HasValue)
            {
                criterias.Add(Expression.Eq("Parent.Id", obj.ParentId));
                res = true;
            }
            if (base.QueryFilterByCategory<AdminRoleInfo, QueryRoleFormViewModel>(ref criterias, obj)) res = true;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            return res;
        }
        protected override List<AdminRoleInfo> DataParseIfWhileReference(List<AdminRoleInfo> data, bool where = false)
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