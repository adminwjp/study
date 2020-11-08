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
using Utility.Domain.Repositories;
using Utility.Response;

namespace OA.Api.Famous
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi),200)]
    public class FamousController : BaseController<FamousRaceInfo>
    {
        public FamousController(ILogger<FamousController> logger, IRepository<FamousRaceInfo> repository) : base(logger,repository)
        {
           
        }
        protected override ResponseApi Edited(FamousRaceInfo obj)
        {
            this.Repository.Update(it => it.Id == obj.Id, it => new FamousRaceInfo() { Name = obj.Name, UpdateDate = DateTime.Now });
            return ResponseApi.CreateSuccess();
        }
    }
}