using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class EdutionInfo:Entry
    {
        public virtual EdutionCategoryInfo EdutionCategory { get; set; }
        public virtual UserInfo User { get; set; }
    
    }
}
