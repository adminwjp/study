using SocialContact.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.Core
{
    public class MenuInfo : Entry,ICasecade<MenuInfo>,IAdmin
    {
        //关键字name 建表失败 
        [Utility.Required(Message = "请输入菜单名称")]
        [Utility.Range(Min = 2, Max = 20, Message = "长度在 2 到 20 个字符菜单名称")]
        [BindProperty(Name ="menu_name")]
        public virtual string MenuName { get; set; }
        //关键字Group 建表失败 
        //[Utility.Required(Message = "请输入菜单分组名称")]
        //[Utility.Range(Min = 2, Max = 20, Message = "长度在 2 到 20 个字符菜单分组名称")]
        [BindProperty(Name = "menu_group")]
        public virtual string MenuGroup { get; set; }
        //[Utility.Range(Min = 2, Max = 50, Message = "长度在 2 到 50 个字符菜单链接地址")]
        public virtual string Href { get; set; }
        public virtual bool Collpse { get; set; }
        public virtual IconInfo Icon { get; set; }
        [Utility.Required(Message = "请输入菜单描述")]
        [Utility.Range(Min = 10, Max = 500, Message = "长度在 10 到 500 个字符菜单描述")]
        public virtual string Description { get; set; }
        public virtual MenuInfo Parent { get; set; }
        public virtual AdminInfo Admin { get; set; }
        public virtual int? OrderNo { get; set; }
        public virtual ISet<MenuInfo> Children { get; set; }
        public virtual UserMenuInfo Menu { get; set; }

        public object Clone()
        {
            return new MenuInfo()
            {
                Id = this.Id,
                MenuName = this.MenuName,
                MenuGroup = this.MenuGroup,
                Href = this.Href,
                Collpse = this.Collpse,
                Description = this.Description
            };
        }
    }
}
