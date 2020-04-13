using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class SkillCategoryInfo:DefaultEntry,ICasecade<SkillCategoryInfo>,IAdmin
    {
        public virtual AdminInfo Admin { get; set; }
        public virtual SkillCategoryInfo Parent { get; set; }
        public virtual ISet<SkillCategoryInfo> Children { get; set; }

        public object Clone()
        {
            return new SkillCategoryInfo() { Id = this.Id, CreateDate = this.CreateDate, UpdateDate = this.UpdateDate, Category = this.Category, Description = this.Description };
        }
        public virtual ICollection<UserInfo> Users { get; set; }
        public virtual ICollection<SkillInfo> Skills { get; set; }
    }
}
