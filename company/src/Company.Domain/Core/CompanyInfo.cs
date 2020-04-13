using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("company_info")]
    public  class CompanyInfo:BaseDesc
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("tel")]
        [System.ComponentModel.DataAnnotations.StringLength(15)]
        public string Tel { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("logo")]
        public ImageInfo Logo { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("logo1")]
        public ImageInfo Logo1 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src1 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
