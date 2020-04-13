using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class JobCategoryInfo:DefaultEntry,ICasecade<JobCategoryInfo>,IAdmin
    {
        public virtual AdminInfo Admin { get; set; }
        public virtual JobCategoryInfo Parent { get; set; }
        public virtual ISet<JobCategoryInfo>  Children { get; set; }

        public object Clone()
        {
            return new JobCategoryInfo() { Id=this.Id,CreateDate=this.CreateDate,UpdateDate=this.UpdateDate,Category=this.Category,Description=this.Description};
        }
    }
}
