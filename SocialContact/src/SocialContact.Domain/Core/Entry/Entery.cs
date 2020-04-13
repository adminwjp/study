using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Core
{
    public abstract class Entry
    {
        public Entry()
        {
            //this.Id = Guid.NewGuid().ToString("N");
        }
        [Utility.OptionValidator(Message = "请输入id",Options =true,Way = Utility.ValidatorWay.ModifyWay)]
        public virtual int? Id { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
