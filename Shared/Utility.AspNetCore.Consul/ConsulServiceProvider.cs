using Consul;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.AspNetCore.Consul.Model;

namespace Utility.AspNetCore.Consul
{
    public class ConsulServiceProvider
    {
        /// <summary>
        /// 发现服务
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<ServiceEntity>> GetHealthServicesAsync(string url,string servicerName)
        {
            var consuleClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(url);
            }); 
             var queryResult = await consuleClient.Health.Service(servicerName);
            var result = new List<ServiceEntity>();
            foreach (var serviceEntry in queryResult.Response)
            {
                result.Add(new ServiceEntity() { 
                    Id= serviceEntry.Service.ID,
                    IP= serviceEntry.Service.Address,
                    Port= serviceEntry.Service.Port,
                    Name= serviceEntry.Service.Service,
                    ConsulIP=serviceEntry.Node.Address
                });
            }
            return result;
        }
        /// <summary>
        /// 发现服务
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<ServiceEntity>> GetAgentServicesAsync(string url)
        {
            var consuleClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(url);
            });
            var queryResult = await consuleClient.Agent.Services();
            var result = new List<ServiceEntity>();
            foreach (var serviceEntry in queryResult.Response)
            {
                result.Add(new ServiceEntity()
                {
                    Id = serviceEntry.Value.ID,
                    IP = serviceEntry.Value.Address,
                    Port = serviceEntry.Value.Port,
                    Name = serviceEntry.Value.Service,
                    ConsulIP = serviceEntry.Value.Address
                });
            }
            return result;
        }
        /// <summary>
        /// 发现服务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="servicerName"></param>
        /// <returns></returns>
        public async Task<List<ServiceEntity>> GetCatalogServicesAsync(string url,string servicerName)
        {
            var consuleClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(url);
            });
            var queryResult = await consuleClient.Catalog.Service(servicerName);
            var result = new List<ServiceEntity>();
            foreach (var catalog in queryResult.Response)
            {
                result.Add(new ServiceEntity()
                {
                    Id = catalog.ServiceID,
                    IP = catalog.ServiceAddress,
                    Port = catalog.ServicePort,
                    Name = catalog.ServiceName,
                    ConsulIP = catalog.Node
                });
            }
            return result;
        }
    }
}
