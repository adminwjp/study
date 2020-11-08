using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using SocialContact.Domain.Interfaces;
using Utility.Randoms;
using Utility.Security.Extensions;

namespace SocialContact.Domain.Core
{
    public class UserFileInfo:Entry,ICloneable,IAdmin
    {

        public virtual string Src { get; set; }

        //public virtual byte[] Base64 { get; set; }
        public virtual string Base64 { get; set; }
        [Utility.Attributes.Required(Message = "请输入描述")]
        [Utility.Attributes.Range(Min = 10, Max = 500, Message = "长度在 10 到 500 个字符描述")]
        public virtual string Description { get; set; } = "这个人很懒,什么也没留下!";
        
        public virtual FileCategoryInfo Category { get; set; }
        public virtual UserFileInfo Parent { get; set; }
        public virtual AdminInfo Admin { get; set; }
        public virtual UserInfo User { get; set; }
        //[FromForm(Name = "src")]
        [FromForm(Name = "file_id")]
        public virtual string FileId { get; set; }
        public virtual string Type { get; set; }

        /// <summary>
        /// 子类
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new UserFileInfo()
            {
                CreateDate = this.CreateDate,
                UpdateDate =this.UpdateDate,
                Parent = this,
                Admin=this.Admin,
                User=this.User,
                FileId = RandomHelper.OrderId.Sha1(),
                Src=$"{ RandomHelper.OrderId.Sha1()}.{this.Src.Split('.').LastOrDefault()}",
                Type=this.Type,
                Category=this.Category
            };
        }
    }
}
