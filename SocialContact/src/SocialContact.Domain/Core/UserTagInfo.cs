
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class UserTagInfo : Entry
    {
        public virtual UserInfo User { get; set; }
        public UserTagCategoryInfo Category { get; set; }
    }
}
