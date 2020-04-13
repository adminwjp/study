using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("skin_info")]
    public class SkinInfo : BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("color")]
        public string Color { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
