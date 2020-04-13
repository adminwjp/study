using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(Utility.ResponseApi), StatusCodes.Status200OK)]
    public class ESController : ControllerBase
    {
        private readonly ElasticsearchDatabase _elasticsearchDatabase;
        private readonly ILogger<ESController> _logger;
        public ESController( ElasticsearchDatabase elasticsearchDatabase, ILogger<ESController> Logger) 
        {
            this._logger = Logger;
            this._elasticsearchDatabase = elasticsearchDatabase;
        }
        [HttpGet("delete")]
        public async Task<Utility.ResponseApi> Delete([FromQuery] string index,[FromQuery]string type,[FromQuery] string id)
        {
            if (string.IsNullOrEmpty(index))
            {
                return await Task.FromResult(ResponseApiUtils.GetResponse(Language.Chinese,Utility.Code.ParamNotNull));
            }
            else
            {
                if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.Delete(index);
                }
                else if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.Delete(index,id);
                }
                else if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.DeleteUseType(index, type);
                }
                else
                {
                     this._elasticsearchDatabase.Delete(index,type, id);
                }
                Utility.ResponseApi response = ResponseApiUtils.GetResponse(Language.Chinese,Utility.Code.DeleteSuccess);
                return await Task.FromResult(response);
            }
        }
    }
}