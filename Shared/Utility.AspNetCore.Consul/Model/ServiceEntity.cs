using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.AspNetCore.Consul.Model
{
    public class ServiceEntity
    {
        public string Id { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string ConsulIP { get; set; }
        public int ConsulPort { get; set; }
        public string[] Tags { get; set; }
    }
}
