using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace OA.Api.Famous
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi),200)]
    public class RoleController : BaseController<RoleInfo>
    {
        public RoleController(ILogger<RoleController> logger, IRepository<RoleInfo> repository) : base(logger,repository)
        {
           
        }
        protected override ResponseApi Edited(RoleInfo obj)
        {
            base.Repository.Update(it => it.Id == obj.Id, it => new RoleInfo() { Name = obj.Name, UpdateDate = DateTime.Now });
            return ResponseApiUtils.Success();
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data=base.Repository.Find(null).Select(it=>new { it.Id,it.Name}).ToList();
            return ResponseApiUtils.Success().SetData(data);
        }
    }
}