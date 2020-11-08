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
using System;
using System.Collections.Generic;
using NHibernate.Linq;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Criterion;
using Utility.Enums;
using Utility.Response;
using Utility.Redis;
using Utility.Domain.Uow;
using Utility.ObjectMapping;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]

    public class UserMenuController : SocialContact.Api.Controllers.BaseController<UserMenuInfo, QueryUserMneuFormViewModel, QueryUserMneuInfoResultViewModel>
    {
        public UserMenuController(IRedisCache redisCache,IObjectMapper objectMapper, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<UserMenuController> logger) : base(redisCache, unitWork,cache, authrize, logger)
        {
            base.ObjectMapper = objectMapper;
            base.IsCustomValidator = true;
            base.PageName = "user_menu";
        }
        [HttpGet("init")]
        public  async Task<ResponseApi> Init()
        {
            AdminInfo admin = base.Test ? null : GetAdminInfo();
            using NHibernate.ISession session = HttpContext.RequestServices.GetService<Utility.Nhibernate.Infrastructure.AppSessionFactory>().OpenSession();
            NHibernate.ITransaction transaction = session.BeginTransaction();
            foreach (var item in session.Query<AdminRoleInfo>().Select(it=>it.Id))
            {
                //session.Query<MenuInfo>().Where(it => it.Menu.Role.Id != item && it.Id != it.Menu.Menu.Id &&
                //       it.Href != null).InsertInto(it => new UserMenuInfo()
                //       {
                //           Admin = admin,
                //           Menu = new MenuInfo() { Id = it.Id },
                //           Role = new AdminRoleInfo() { Id = item }
                //       });
                foreach (var  menu in session.Query<MenuInfo>().Where(it=>it.Href!=null).Select(it => it.Id))
                {
                    if (session.Query<UserMenuInfo>().Where(it => it.Menu.Id == menu && it.Role.Id == item).ToFutureValue(it => it.Count()).Value == 0)
                    {
                        session.Save(new UserMenuInfo()
                        {
                            Admin = admin,
                            Menu = new MenuInfo() { Id = menu },
                            Role = new AdminRoleInfo() { Id = item }
                        });
                    }
                }
            }
            foreach (var item in session.Query<UserLevelInfo>().Select(it => it.Id))
            {
                foreach (var menu in session.Query<MenuInfo>().Where(it => it.Href != null).Select(it => it.Id))
                {
                    if (session.Query<UserMenuInfo>().Where(it => it.Menu.Id == menu && it.Level.Id == item).ToFutureValue(it => it.Count()).Value == 0)
                    {
                        session.Save(new UserMenuInfo()
                        {
                            Admin = admin,
                            Menu = new MenuInfo() { Id = menu },
                            Level = new UserLevelInfo() { Id = item }
                        });
                    }
                }
            }
            transaction.Commit();
            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.OperatorSuccess));
        }
        protected override Func<UserMenuInfo, CategoryEntry> Select()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Menu.MenuName };
        }
        protected override List<UserMenuInfo> DataParseIfWhileReference(List<UserMenuInfo> data, bool where = false)
        {
            foreach (var item in data)
            {
                item.Clear();
            }
            return base.DataParseIfWhileReference(data, where);
        }
        protected override bool QueryFilterByOr(ref List<AbstractCriterion> criterias, QueryUserMneuFormViewModel obj)
        {
            bool res = false;
            if (base.QueryFilterByOr(ref criterias, obj)) res = true;
            if (obj.MentId.HasValue)
            {
                criterias.Add(Expression.Eq("Menu.Id", obj.MentId));
                res = true;
            }
            if (obj.LevelId.HasValue)
            {
                criterias.Add(Expression.Eq("Level.Id", obj.LevelId));
                res = true;
            }
            if (obj.RoleId.HasValue)
            {
                criterias.Add(Expression.Eq("Role.Id", obj.RoleId));
                res = true;
            }
            return res;
        }
        [HttpPost("add")]
        public override async Task<ResponseApi> Add([ FromForm,FromBody] UserMenuInfo obj)
        {
            return await Task.FromResult(ResponseApi.Create(GetLanguage(),Code.NotSupport,false));
        }
        protected override async Task<ResponseApi> EditMiddlewareExecuted(UserMenuInfo obj)
        {
            int res = 0;
            switch (obj.Type?.ToLower())
            {
                case "enable":
                    res = this.UnitWork.Find<UserMenuInfo>(it => it.Id == obj.Id).Update<UserMenuInfo>(it => new UserMenuInfo()
                    {
                        Enable = obj.Val,
                        Add = obj.Val,
                        Modify = obj.Val,
                        Delete = obj.Val,
                        Query = obj.Val
                    });
                    break;
                case "add":
                    res = this.UnitWork.Find<UserMenuInfo>(it => it.Id == obj.Id).Update<UserMenuInfo>(it => new UserMenuInfo()
                    {
                        Enable = obj.Val ? true : it.Enable,
                        Add = obj.Val,
                    });
                    break;
                case "modify":
                    res = this.UnitWork.Find<UserMenuInfo>(it => it.Id == obj.Id).Update<UserMenuInfo>(it => new UserMenuInfo()
                    {
                        Enable = obj.Val ? true : it.Enable,
                        Modify = obj.Val,
                    });
                    break;
                case "delete":
                    res = this.UnitWork.Find<UserMenuInfo>(it => it.Id == obj.Id).Update<UserMenuInfo>(it => new UserMenuInfo()
                    {
                        Enable = obj.Val ? true : it.Enable,
                        Delete = obj.Val,
                    });
                    break;
                case "query":
                    res = this.UnitWork.Find<UserMenuInfo>(it => it.Id == obj.Id).Update<UserMenuInfo>(it => new UserMenuInfo()
                    {
                        Enable = obj.Val ? true : it.Enable,
                        Query = obj.Val,
                    });
                    break;
                default: return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.NotSupportOperator)); 
            }
           return await Task.FromResult(ResponseApi.Create(GetLanguage(),res>0?Code.OperatorSuccess:Code.OperatorFail,res>0));
        }
        [HttpPost("category")]
        public override async Task<ResponseApi> Category()
        {
            return await Task.FromResult(ResponseApi.Create(GetLanguage(), Code.NotSupport, false));
        }
    }
}