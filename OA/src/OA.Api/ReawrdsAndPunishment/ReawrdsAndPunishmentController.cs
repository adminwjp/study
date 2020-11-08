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
    [Route("api/v{version:apiVersion}/reawrds_and_punishment")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class ReawrdsAndPunishmentController : BaseController<ReawrdsAndPunishmentInfo>
    {
        public ReawrdsAndPunishmentController(ILogger<ReawrdsAndPunishmentController> logger, IRepository<ReawrdsAndPunishmentInfo> repository) : base(logger, repository)
        {

        }
    }
}