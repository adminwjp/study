using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("social_info")]
    public class SocialInfo:BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("icon")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Icon { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("href")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Href { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("company_id")]
        public CompanyInfo Company { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("parent_id")]
        public SocialInfo Parent { get; set; }
        public ICollection<SocialInfo> Children { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
