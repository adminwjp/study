using Consul;
using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.AspNetCore.Consul.Model;

namespace Utility.AspNetCore.Consul.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app,  ServiceEntity serviceEntity)
        {
            serviceEntity.IP= NetworkUtils.LocalIp; 
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{serviceEntity.ConsulIP}:{serviceEntity.ConsulPort}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{serviceEntity.IP}:{serviceEntity.Port}/api/health",//健康检查地址
                // 超时时间
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = serviceEntity.Name,
                Address = serviceEntity.IP,
                Port = serviceEntity.Port,
                Tags = serviceEntity.Tags//添加  格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            Stop = () =>
            {
                 consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            };

            return app;
        }
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            if(bool.TryParse(configuration["EnableService"],out bool enable))
            {
                if (!enable)
                {
                    return app;
                }
            }
            string ip = NetworkUtils.LocalIp;
            int port = Convert.ToInt32(configuration["Service:Port"]);
            string ServiceName = configuration["Service:Name"];
            string consulIP = configuration["Service:ConsulIP"];
            int consulPort = Convert.ToInt32(configuration["Service:ConsulPort"]);
            var consulClient = new ConsulClient(x => x.Address = new Uri($"http://{consulIP}:{consulPort}"));//请求注册的 Consul 地址
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔，或者称为心跳间隔
                HTTP = $"http://{ip}:{port}/api/health",//健康检查地址
                Timeout = TimeSpan.FromSeconds(5)
            };

            // Register service with consul
            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = Guid.NewGuid().ToString(),
                Name = ServiceName,
                Address = ip,
                Port = port,
                Tags = configuration.GetValue<string[]>("Service:Tags")//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
            };

            consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
            AppBuilderExtensions.Stop = () => {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            };
            return app;
        }
        public static void StopConsul(this IApplicationBuilder applicationBuilder)
        {
            AppBuilderExtensions.Stop?.Invoke();
        }
        internal static Action Stop { get; set; }
    }
}
