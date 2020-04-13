using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("image_info")]
    public class ImageInfo : BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("href")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Href { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("src")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("type")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Type { get; set; }

    }
}
