using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("media_info")]
    public  class MediaInfo:BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("body")]
        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        public string Body { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_body")]
        [System.ComponentModel.DataAnnotations.StringLength(1000)]
        [BindProperty(Name = "english_body")]
        public string EnglishBody { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("src")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
