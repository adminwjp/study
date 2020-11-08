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
    [Route("api/v{version:apiVersion}/time_card")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class TimeCardController : BaseController<TimeCardInfo>
    {
        public TimeCardController(ILogger<TimeCardController> logger, IRepository<TimeCardInfo> repository) : base(logger, repository)
        {

        }
       
    }
}