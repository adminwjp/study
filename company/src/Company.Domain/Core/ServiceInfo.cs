using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("service_info")]
    public class ServiceInfo : BaseDesc
    {
        //[System.ComponentModel.DataAnnotations.Schema.Column("img")]
        //[System.ComponentModel.DataAnnotations.StringLength(100)]
        //public string Img { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("img")]
        public ImageInfo Img { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
  
}
