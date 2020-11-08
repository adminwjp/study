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
using Utility.Domain.Repositories;
using Utility.Response;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/reckoning_name")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReckoningNameController : BaseController<ReckoningNameInfo>
    {
        public ReckoningNameController(ILogger<ReckoningNameController> logger, IRepository<ReckoningNameInfo> repository) : base(logger, repository)
        {

        }
		protected override ResponseApi Edited(ReckoningNameInfo obj)
        {
            base.Repository.Update(it => it.Id == obj.Id, it => new ReckoningNameInfo() { Name = obj.Name,Explain=obj.Explain, UpdateDate = DateTime.Now });
            return ResponseApi.CreateSuccess();
        }
	    [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApi.CreateSuccess().SetData(data);
        }
    }
}