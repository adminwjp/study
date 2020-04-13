using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.AspNetCore.Consul.Model;

namespace Utility.AspNetCore.Consul
{
    [Produces("application/json")]
    [Route("api/Health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ServiceEntity _serviceEntity;
        public HealthController(IOptions<ServiceEntity>  serviceOptions)
        {
            this._serviceEntity = serviceOptions.Value;
        }
        [HttpGet]
        public IActionResult Get() => Ok("ok");
        [HttpGet("service/{name}")]
        public async Task<List<ServiceEntity>> GetHealthService(string name) {
            return await new ConsulServiceProvider().GetHealthServicesAsync($"http://{this._serviceEntity.ConsulIP}:{this._serviceEntity.ConsulPort}",name);
        }
        [HttpGet("catalog/{name}")]
        public async Task<List<ServiceEntity>> GetCatalogService(string name)
        {
            return await new ConsulServiceProvider().GetCatalogServicesAsync($"http://{this._serviceEntity.ConsulIP}:{this._serviceEntity.ConsulPort}", name);
        }
    }
}
