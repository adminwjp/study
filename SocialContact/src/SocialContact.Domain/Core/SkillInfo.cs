using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class SkillInfo:Entry
    {
        public virtual SkillCategoryInfo  Category { get; set; }
        public virtual UserInfo User { get; set; }
    }
}
