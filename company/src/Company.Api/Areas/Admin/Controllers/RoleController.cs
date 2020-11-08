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
using Utility.Response;
using Utility.Domain.Repositories;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/admin/[controller]")]
    public class RoleController : BaseController<AdminRoleInfo>
    {
        public RoleController(IRepository<AdminRoleInfo> repository, ILogger<RoleController> logger):base(repository,logger)
        {

        }
        protected override void AddMiddleExecet(AdminRoleInfo obj)
        {
           
        }
        protected override void EditMiddleExecet(AdminRoleInfo obj)
        {
            this.AddMiddleExecet(obj);
        }
        protected override List<AdminRoleInfo> QueryList(AdminRoleInfo obj, int? page, int? size)
        {
            List<AdminRoleInfo> datas = this.Query(QueryFilter(null,obj)).Include(it=>it.Admins)/*.Skip((page.Value - 1) * size.Value).Take(size.Value)*/.ToList();
            return datas;
        }
        [HttpGet("category")]
        public virtual async Task<ResponseApi> Category()
        {
            ResponseApi responseApi = ResponseApi.CreateSuccess(GetLanguage());
            var data = base.Repository.Find(null).Select(it => new AdminRoleInfo()
            {
                Id = it.Id,
                Name = it.Name,
                EnglishName = it.EnglishName
            });
            responseApi.Data = data;
            return await Task.FromResult<ResponseApi>(responseApi);
        }
    }
}