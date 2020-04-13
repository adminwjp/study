using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using System.Xml.Serialization;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/bring_up_content")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BringUpContentController : BaseController<BringUpContentInfo>
    {
        public BringUpContentController(ILogger<BringUpContentController> logger, IRepository<BringUpContentInfo> repository) : base(logger, repository)
        {

        }
        protected override ResponseApi Edited(BringUpContentInfo obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new BringUpContentInfo() { UpdateDate = DateTime.Now, Name = obj.Name, Content = obj.Content, Unit = obj.Unit, Place = obj.Place, StartDate = obj.StartDate, EndDate = obj.EndDate });
            return ResponseApiUtils.Success();
        }
		  [HttpGet("category")]
        public ResponseApi Category()
        {
            var data = base.Repository.Find(null).Select(it => new { it.Id, it.Name }).ToList();
            return ResponseApiUtils.Success().SetData(data);
        }
    }
}