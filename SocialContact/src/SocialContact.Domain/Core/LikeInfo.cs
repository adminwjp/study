using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class LikeInfo:Entry
    {
        public virtual LikeCategoryInfo Category { get; set; }
        public virtual UserInfo User { get; set; }

    }
}
