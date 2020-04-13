using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Models
{
    public class AuthrizeValidator
    {
        /// <summary>
        /// 角色必选(父角色)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RoleRequired(int id)
        {
            return true;
        }
        public OperatorEntry GetOperatorAuthrize(int id, string page)
        {
            return new OperatorEntry() {Add=true,Modify=true,Delete=true,Query=true };
        }
    }
    public class OperatorEntry
    {
        public bool Add { get; set; }
        public bool Modify { get; set; }
        public bool Delete { get; set; }
        public bool Query { get; set; }
    }
}
