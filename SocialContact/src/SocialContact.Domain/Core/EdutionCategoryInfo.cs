using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class EdutionCategoryInfo : DefaultEntry, IAdmin
    {
        public virtual AdminInfo Admin { get; set; }
        public virtual ISet<UserInfo> Users { get; set; }
    }
}
