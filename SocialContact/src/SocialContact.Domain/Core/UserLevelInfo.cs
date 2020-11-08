using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class UserLevelInfo:Entry,IAdmin
    {
        [Utility.Attributes.Required(Message = "请输入名称")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "长度在 2 到 10 个字符名称")]
        public virtual string Name { get; set; }
        [Utility.Attributes.Required(Message = "请输入描述")]
        [Utility.Attributes.Range(Min = 10, Max = 500, Message = "长度在 10 到 500 个字符名称描述")]
        public virtual string Description { get; set; }
        public virtual AdminInfo Admin { get; set; }
        public virtual ISet<UserInfo> Users { get; set; }

    }
}
