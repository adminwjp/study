using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Domain.Repositories;
using Utility.Response;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ModuleController : BaseController<ModuleInfo>
    {
        public ModuleController(ILogger<ModuleController> logger, IRepository<ModuleInfo> repository) : base(logger, repository)
        {

        }
		protected override ResponseApi Edited(ModuleInfo obj)
		{
			base.Repository.Update(it => it.Id == obj.Id, it => new ModuleInfo() { Name = obj.Name, Href = obj.Href, Parent = obj.Parent, UpdateDate = DateTime.Now });
			return ResponseApi.CreateSuccess();
		}
		[HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(it=>it.Parent.Id>0).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}