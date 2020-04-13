using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("menu_info")]
    public class MenuInfo:BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("icon")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Icon { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("href")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Href { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("id_name")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string IdName { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("parent_id")]

        public MenuInfo Parent { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //属性名称跟name不一致则创建新的列名
        [FromForm(Name = "parent_id")]
        public int? ParentId { get; set; }
        public ICollection<MenuInfo> Children { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
