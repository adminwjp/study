using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class LikeCategoryInfo:DefaultEntry,ICasecade<LikeCategoryInfo>,IAdmin
    {
        public virtual AdminInfo Admin { get; set; }
        public virtual LikeCategoryInfo Parent { get; set; }
        public virtual ISet<LikeCategoryInfo> Children { get; set; }

        public object Clone()
        {
            return new LikeCategoryInfo() { Id = this.Id, CreateDate = this.CreateDate, UpdateDate = this.UpdateDate, Category = this.Category, Description = this.Description };
        }
        public virtual ICollection<UserInfo> Users { get; set; }
        public virtual ICollection<LikeInfo>  Likes { get; set; }
    }
}
