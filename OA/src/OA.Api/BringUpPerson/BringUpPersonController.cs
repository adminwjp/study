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
    [Route("api/v{version:apiVersion}/bring_up_person")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BringUpPersonController : BaseController<BringUpPersonInfo>
    {
        public BringUpPersonController(ILogger<BringUpPersonController> logger, IRepository<BringUpPersonInfo> repository) : base(logger, repository)
        {

        }
    }
}