using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class FileCategoryInfo:DefaultEntry,IAdmin
    {
        public virtual AdminInfo Admin { get; set; }
        public virtual ISet<UserFileInfo>  Files { get; set; }
        [Utility.Attributes.Required(Message = "请输入接受文件后缀类型")]
        [Utility.Attributes.Range(Min = 2, Max = 50, Message = "长度在 2 到 50 个字符接受文件后缀类型")]
        public virtual string Accept { get; set; }
    }
}
