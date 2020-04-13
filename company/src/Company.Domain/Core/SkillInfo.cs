using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("skill_info")]
    public class SkillInfo:BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("process")]
        public int Process { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("style")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Style { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
  
}
