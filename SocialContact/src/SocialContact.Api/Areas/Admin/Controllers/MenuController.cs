using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using Utility;
using Microsoft.Extensions.Caching.Memory;
using SocialContact.Api.Exceptions;
using NHibernate.Criterion;
using NHibernate;
using SocialContact.Api.Models;
using Utility.Response;
using Utility.Enums;
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

    public class MenuController : SocialContact.Api.Controllers.BaseController<MenuInfo, QueryMenuFormViewModel, QueryMenuInfoResultViewModel>
    {
        public MenuController(IRedisCache redisCache,IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<MenuController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "menu";
        }
        protected override MenuInfo EditMiddlewareExecute(MenuInfo obj)
        {
            obj = base.EditMiddlewareExecute(obj);
            base.SetAdmin(obj);
            if (obj.Parent == null ||  !obj.Parent.Id.HasValue)
            {
                obj.Parent = obj;
                var oldObj = UnitWork.FindSingle<MenuInfo>(it => it.Id == obj.Id);
                UnitWork.Find<MenuInfo>(it => it.OrderNo > oldObj.OrderNo).Update(it =>  new MenuInfo()
                {
                    OrderNo = it.OrderNo - 1
                });
                obj.OrderNo = UnitWork.Count<MenuInfo>(it=>it.Id==it.Parent.Id) + 1;
            }
            else
            {
                var oldObj = UnitWork.FindSingle<MenuInfo>(it => it.Id == obj.Id);
                if (oldObj.Parent.Id != obj.Parent.Id)
                {
                    MenuInfo parent = UnitWork.FindSingle<MenuInfo>(it => it.Id == obj.Parent.Id);
                    int childCount = UnitWork.Count<MenuInfo>(it => it.Parent.Id == obj.Parent.Id);
                    UnitWork.Find<MenuInfo>(it => it.OrderNo > oldObj.OrderNo).Update(it => new MenuInfo()
                    {
                        OrderNo = it.OrderNo + 1
                    });
                    obj.OrderNo = UnitWork.Count<MenuInfo>(it => it.OrderNo < parent.OrderNo) + 1 + childCount + 1;
                }
            }
    
            if (obj.Icon != null && !obj.Icon.Id.HasValue) obj.Icon = null;
            return obj;
        }
        protected override MenuInfo AddMiddlewareExecute(MenuInfo obj)
        {
            obj = base.AddMiddlewareExecute(obj);
            base.SetAdmin(obj);
            if (obj.Parent==null||(obj.Parent != null && !obj.Parent.Id.HasValue))
            {
                obj.Parent = obj;
                obj.OrderNo = UnitWork.Count<MenuInfo>(it=>it.Id==it.Parent.Id) + 1;
            }
            else
            {
                obj.OrderNo = UnitWork.Count<MenuInfo>(it=>it.Parent.Id==it.Parent.Id) + 1;
            }
            if (obj.Icon != null && !obj.Icon.Id.HasValue) obj.Icon = null;
            return obj;
        }
        protected override IQueryable<MenuInfo> QueryChildFilter(IQueryable<MenuInfo> query, QueryMenuFormViewModel obj)
        {
            query = base.QueryChildFilter(query);
            return query;
        }
        protected override void DeleteBefore(int id)
        {
            MenuInfo menu = UnitWork.FindSingle<MenuInfo>(it => it.Id == id);
            if (menu==null) throw new IdNotFoundException(ResponseApi.Create(GetLanguage(),Code.IdNotFound,false).Message);
            if (menu.Id == menu.Parent.Id)
            {
                if (menu.Children.Count>1)
                {
                    throw new CascadeException(ResponseApi.Create(GetLanguage(), Code.CascadeDeleteFail, false).Message);
                }
            }
            else
            {
                Func<MenuInfo,MenuInfo> func = (it) =>{
                    it.OrderNo -= 1;
                    return it;
                };
                UnitWork.Find<MenuInfo>(it => it.OrderNo>menu.OrderNo).Update(it=> func(it));
            }
            base.DeleteBefore(id);
        }
        protected override void DeleteBefore(int[] id)
        {
            IQueryable<MenuInfo> query = UnitWork.Find<MenuInfo>(null);
            foreach (var item in id)
            {
                query = query.Where(it => it.Id == item);
            }
            List<MenuInfo> datas = query.ToList();
            foreach (var item in datas)
            {
                if (item.Id == item.Parent.Id)
                {
                    if (item.Children.Any())
                    {
                        throw new CascadeException(ResponseApi.Create(GetLanguage(), Code.CascadeDeleteFail, false).Message);
                    }
                }
                Func<MenuInfo, MenuInfo> func = (it) => {
                    it.OrderNo -= 1;
                    return it;
                };
                UnitWork.Find<MenuInfo>(it => it.OrderNo > item.OrderNo).Update(it => func(it));
            }
            base.DeleteBefore(id);
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryMenuFormViewModel obj)
        {
            bool res = false;
            if (!string.IsNullOrEmpty(obj.MenuName))
            {
                criterias.Add(Expression.Like("MenuName", $"%{obj.MenuName}%"));
                res = true;
            }
            if (!string.IsNullOrEmpty(obj.MenuGroup))
            {
                criterias.Add(Expression.Like("MenuGroup", $"%{obj.MenuGroup}%"));
                res = true;
            }
            if (obj.ParentId.HasValue)
            {
                criterias.Add(Expression.Eq("Id", obj.ParentId));
                res = true;
            }
            if (base.QueryFilterByOr(ref criterias, obj))
            {
                res = true;
            }
            return res;
        }
        protected override void OrderBy(ref ICriteria criteria)
        {
            criteria=criteria.AddOrder(Order.Asc("OrderNo"));
           //base.OrderBy(ref criteria);
        }
        [HttpGet("category")]

        public override async Task<ResponseApi> Category ()
        {
            //Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<List<QueryMenuInfoResultViewModel>> jsonPatch = new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<List<QueryMenuInfoResultViewModel>>();
            List<MenuInfo> menus = UnitWork.Find<MenuInfo>(it=>it.Id==it.Parent.Id ||!it.Parent.Id.HasValue).OrderBy(it=>it.OrderNo).ToList();
            menus = DataParseIfWhileReference(menus);
            List<MenuViewModel> viewModels = ObjectMapper.Map<List<MenuViewModel>>(menus);
            //jsonPatch.ApplyTo(iconViewModels);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = viewModels;
            return await Task.FromResult(response);
        }
        [HttpGet("parent_category")]
        public  async Task<ResponseApi> QueryMenu()
        {
            List<MenuInfo> menuInfos = UnitWork.Find<MenuInfo>(null).Select(it=>new MenuInfo() { Id=it.Id,MenuName=it.MenuName,OrderNo=it.OrderNo,Icon=new IconInfo() { Style=it.Icon.Style} }).ToList();
            menuInfos = DataParseIfWhileReference(menuInfos);
            List<MenuViewModel> viewModels = ObjectMapper.Map<List<MenuViewModel>>(menuInfos);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = viewModels;
            return await Task.FromResult(response);
        }
        protected override List<MenuInfo> DataParseIfWhileReference(List<MenuInfo> data, bool where = false)
        {
            if (data.Any())
            {
                if (where)
                {
                    data= RecursionDataParseByWhere(data);
                }
                else
                {
                    data= RecursionDataParse(data, null);
                }
                //手动排序，位置不对,菜单固定位置
                data = RecursionOrderBy(data);
                return data;
            }
            else
            {
                return data;
            }
        }
       protected override List<MenuInfo> RecursionOrderBy(List<MenuInfo> result) {
            result.Sort(Comparer<MenuInfo>.Create((x,y)=> {
                if (x.OrderNo == y.OrderNo) return 0;
                else if (x.OrderNo > y.OrderNo) return 1;
                return -1;
            }));
            foreach (var item in result)
            {
                if (item.Children!=null&&item.Children.Any())
                {
                    item.Children=RecursionOrderBy(item.Children.ToList()).ToHashSet();
                }
            }
            return result;
        }
    }
}