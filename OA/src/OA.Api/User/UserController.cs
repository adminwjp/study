using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Response;
using Utility.Domain.Repositories;

namespace OA.Api.User
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class UserController : BaseController<UserInfo>
    {
        public UserController(ILogger<UserController> logger, IRepository<UserInfo> repository) : base(logger, repository)
        {

        }
        protected override ResponseApi Edited(UserInfo obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new UserInfo() { UpdateDate = DateTime.Now, Role = obj.Role, Password = obj.Password });
            return ResponseApi.CreateSuccess();
        }
        [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Account }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}