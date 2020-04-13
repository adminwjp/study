using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Core
{
    public class UserTagCategoryInfo:DefaultEntry,ICasecade<UserTagCategoryInfo>,IAdmin
    {
        public virtual UserTagCategoryInfo  Parent { get; set; }
        public virtual AdminInfo Admin { get; set; }
        public virtual ISet<UserTagInfo> UserTags { get; set; }
        public virtual ISet<UserTagCategoryInfo> Children { get; set; }

        public object Clone()
        {
            return new UserTagCategoryInfo()
            {
                Id=this.Id,
                CreateDate=this.CreateDate,
                UpdateDate=this.UpdateDate,
                Category=this.Category,
                Description=this.Description
            };
        }
    }
}
