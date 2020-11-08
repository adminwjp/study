using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Domain.Entities;

namespace SocialContact.Domain.Core
{
    public abstract class Entry:IEntity<int?>
    {
        public Entry()
        {
            //this.Id = Guid.NewGuid().ToString("N");
        }
        [Utility.Attributes.OptionValidator(Message = "请输入id",Options =true,Way = Utility.Attributes.ValidatorWay.ModifyWay)]
        public virtual int? Id { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
