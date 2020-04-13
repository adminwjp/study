using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Core
{
    public class CityInfo:Entry
    {
        public string Prodive { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public int ProdiveCode { get; set; }
        public int CityCode { get; set; }
        public int AreaCode { get; set; }
        public AdminInfo AdminInfo { get; set; }
    }
}
