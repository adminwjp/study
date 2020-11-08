using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.Domain.Core;
using Utility;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Company.Domain;
using Microsoft.Extensions.DependencyInjection;
using Utility.Domain.Repositories;
using Utility.Ef.Repositories;
using Utility.Response;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class MenuController : BaseController<MenuInfo>
    {
        public MenuController(IRepository<MenuInfo> repository, ILogger<MenuController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(MenuInfo obj)
        {
            if (obj.Category != null && obj.Category.Id.HasValue)
            {
                if (obj.ParentId.HasValue&&obj.ParentId!=obj.Id)
                {
                    obj.Category = null;
                    obj.Parent = (((BaseEfRepository<MenuInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Menus.Find(new object[] { obj.ParentId});
                }
                else
                {
                    obj.Category = (((BaseEfRepository<MenuInfo>)base.Repository).DbContext as Company.Domain.CompanyDbContext).Categories.Find(new object[] { obj.Category.Id });
                }
            }
           
        }
        protected override void EditMiddleExecet(MenuInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override IQueryable<MenuInfo> Query(Expression<Func<MenuInfo, bool>> expression = null)
        {
            var context = HttpContext.RequestServices.GetService<CompanyDbContext>();
            var data = expression==null? from menu in context.Menus
                        join child in context.Menus on menu.Id equals
                           child.Id
                      join category in context.Categories on child.Category.Id equals category.Id
                       into items
                      from it in items.DefaultIfEmpty()
                      select new
                     {
                           it,
                          child
                      }
                      :
                       from menu in context.Menus.Where(expression)
                       join child in context.Menus.Where(expression) on menu.Id equals
                          child.Id 
                       join category in context.Categories on child.Category.Id equals category.Id
                        into items
                       from it in items.DefaultIfEmpty()
                       select new
                       {
                           it,
                           child
                       }
                      ;
           // data = data/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/;
            //return base.Query(expression).Include(it => it.Children).Include(it=>it.Category);
            var result = ParseData(data);
            result.Sort(Comparer<MenuInfo>.Create((it, it2) => { return it2.Id.Value - it.Id.Value; }));
            return result.AsQueryable();
        }
        protected override List<MenuInfo> QueryList(MenuInfo obj, int? page, int? size)
        {
            List<MenuInfo> datas = this.Query(QueryFilter(null,obj))/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return datas;
        }
        private List<MenuInfo> ParseData(IQueryable<dynamic> dynamics)
        {
            List<CategoryInfo> categoryInfos = new List<CategoryInfo>();
            foreach (var item in dynamics)
            {
                if (item.it != null)
                {
                    CategoryInfo temp = null;
                    if ((temp = categoryInfos.Find(it => it.Id == item.it.Id)) != null)
                    {
                        //using IEnumerator<MenuInfo> enumerator = item.it.Menus.GetEnumerator();
                        //while (enumerator.MoveNext())
                        //    temp.Menus.Add(enumerator.Current);
                    }
                    else
                    {
                        categoryInfos.Add(item.it);
                    }
                }
            }
            List<MenuInfo> menus = new List<MenuInfo>();
            foreach (var item in categoryInfos)
            {
                if (item.Menus.Any())
                {
                    foreach (var menu in item.Menus)
                    {
                        if(menu.Children!=null&& menu.Children.Any())
                        {
                            foreach (var chil in menu.Children)
                            {
                                chil.Category = item;
                                menus.Add(chil);
                                chil.ParentId = menu.Id;
                            }
                        }
                        menu.ParentId = menu.Id;
                        menu.Children = null;
                        menu.Category = item;
                        item.Menus = null;
                        menus.Add(menu);
                    }
                }
            }
            return menus;
        }

        [HttpGet("get")]
        public virtual async Task<ResponseApi> Get()
        {
            ResponseApi ResponseApi = ResponseApi.CreateSuccess(GetLanguage());
            ResponseApi.Data = this.Query().ToList();
            return await Task.FromResult<ResponseApi>(ResponseApi);
        }
    }
}