using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace OA.Api.Authority
{
    [Route("api/v{version:apiVersion}/reckoning_list")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReckoningListController : BaseController<ReckoningListInfo>
    {
        public ReckoningListController(ILogger<ReckoningListController> logger, IRepository<ReckoningListInfo> repository) : base(logger, repository)
        {

        }
    }
}