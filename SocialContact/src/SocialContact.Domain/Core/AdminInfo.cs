using SocialContact.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SocialContact.Domain.Core
{
    public class AdminInfo:Entry,ICasecade<AdminInfo>
    {
        [Utility.Attributes.Required(Message = "请输入账户")]
        [Utility.Attributes.Range(Min = 5, Max = 10, Message = "长度在 5 到 10 个字符账户")]
        public virtual string Account { get; set; }
        [Utility.Attributes.Required(Message = "请输入密码")]
        [Utility.Attributes.Range(Min = 6, Max = 20, Message = "长度在 6 到 20 个字符密码")]
        public virtual string Password { get; set; }
        [Utility.Attributes.Required(Message = "请输入昵称")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "长度在 2 到 10 个字符昵称")]
        [FromForm(Name ="nick_name")]
        public virtual string NickName { get; set; }

        public virtual AdminRoleInfo Role { get; set; }
        [FromForm(Name = "login_date")]
        public virtual DateTime? LoginDate { get; set; }
        public virtual string Token { get; set; }
        public virtual int ExpressIn { get; set; }
        [Utility.Attributes.Required(Message = "请输入姓名")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "长度在 2 到 10 个字符姓名")]
        [FromForm(Name = "real_name")]
        public virtual string RealName { get; set; }
        [Utility.Attributes.Required(Message = "请选择日期")]
        public virtual DateTime? Birthday { get; set; }
        [Utility.Attributes.Required(Message = "请输入手机号")]
        [Utility.Attributes.Range(Min = 11, Max = 11, Message = "长度在 11 个数字手机号")]
        public virtual string Phone { get; set; }
        [Utility.Attributes.Required(Message = "请选择性别",Options =new string[] { "男","女"})]
        public virtual string Sex { get; set; }
        [Utility.Attributes.Required(Message = "请输入邮箱")]
        [Utility.Attributes.Range(Min = 10, Max = 20, Message = "长度在 10 到 20 个字符邮箱")]
        public virtual string Email { get; set; }
        [Utility.Attributes.Required(Message = "请输入描述")]
        [Utility.Attributes.Range(Min = 10, Max = 500, Message = "长度在 10 到 500 个字符描述")]
        public virtual string Description { get; set; }
        public virtual string LoginIp { get; set; }
        [BindProperty(Name = "admin_pic")]
        [JsonProperty(PropertyName = "admin_pic")]
        public virtual UserFileInfo HeadPic { get; set; }
      
        public virtual AdminInfo Parent { get; set; }
        public virtual ISet<AdminInfo> Children { get; set; }
        public ISet<string> LoginIps { get; set; }

        public object Clone()
        {
            return new AdminInfo() { 
                Id=this.Id,
                Account=this.Account,
                NickName=this.NickName,
                Role= this.Role==null?null:new AdminRoleInfo() { Category=this.Role.Category},
                RealName=this.RealName,
                Phone=this.Phone,
                Birthday=this.Birthday,
                Sex=this.Sex,
                Description=this.Description,
                Email=this.Email
            };
        }
    }
}
