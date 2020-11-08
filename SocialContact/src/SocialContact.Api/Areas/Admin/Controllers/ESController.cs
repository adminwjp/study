using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.Enums;
using Utility.Es;
using Utility.Response;

namespace SocialContact.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    public class ESController : ControllerBase
    {
        private readonly ElasticsearchHelper _elasticsearchDatabase;
        private readonly ILogger<ESController> _logger;
        public ESController(ElasticsearchHelper elasticsearchDatabase, ILogger<ESController> Logger) 
        {
            this._logger = Logger;
            this._elasticsearchDatabase = elasticsearchDatabase;
        }
        [HttpGet("delete")]
        public async Task<ResponseApi> Delete([FromQuery] string index,[FromQuery]string type,[FromQuery] string id)
        {
            if (string.IsNullOrEmpty(index))
            {
                return await Task.FromResult(ResponseApi.Create(Language.Chinese,Code.ParamNotNull));
            }
            else
            {
                if (string.IsNullOrEmpty(type) && string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.DeleteDatabase(index);
                }
                else if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.Delete(index,id);
                }
                else if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(id))
                {
                    this._elasticsearchDatabase.Delete(index, type, "{\"query\":{\"match_all\":{}}}");
                }
                else
                {
                     this._elasticsearchDatabase.Delete(index,type, id);
                }
                ResponseApi response = ResponseApi.Create(Language.Chinese,Code.DeleteSuccess);
                return await Task.FromResult(response);
            }
        }
    }
}