using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("theme_info")]
    public class ThemeInfo:BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("href")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Href { get; set; } = "#";
        /// <summary>
        /// 1:公司 2:支持 3 :开发者 4 :我们的合作伙伴
        /// </summary>
        //[System.ComponentModel.DataAnnotations.Schema.Column("Category",TypeName = "int")]
        //public CompanyCategory Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
    public enum CompanyCategory
    {
        COMPANY=1,
        SUPPORT=2,
        DEVELOPERS=3,
        OUR_PARTNERS=4
    }
}
