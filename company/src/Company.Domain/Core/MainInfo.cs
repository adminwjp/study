using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    /// <summary>
    /// 官网主界面 消息
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("main_info")]
    public  class MainInfo:BaseDesc,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public new string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_name")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [FromForm(Name = "english_name")]
        public new string EnglishName { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("background_image")]
        public ImageInfo BackgroundImage { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Bg { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("button_href1")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [FromForm(Name = "button_href1")]
        public string ButtonHref1 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("button_name1")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        [FromForm(Name = "button_name1")]
        public string ButtonName1 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_button_name1")]
        [System.ComponentModel.DataAnnotations.StringLength(30)]
        [FromForm(Name = "english_button_name1")]
        public string EnglishButtonName1 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("button_href2")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [FromForm(Name = "button_href2")]
        public string ButtonHref2 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("button_name2")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        [FromForm(Name = "button_name2")]
        public string ButtonName2 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_button_name2")]
        [System.ComponentModel.DataAnnotations.StringLength(30)]
        [FromForm(Name = "english_button_name2")]
        public string EnglishButtonName2 { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }

        public object Clone()
        {
            return new MainInfo()
            {
                Id = this.Id,
                Name = this.Name,
                EnglishName = this.EnglishName,
                Description = this.Description,
                EnglishDescription = this.EnglishDescription,
                CreateDate = this.CreateDate,
                ModifyDate = this.ModifyDate,
                ButtonName1 = this.ButtonName1,
                EnglishButtonName1 = this.EnglishButtonName1,
                ButtonHref1 = this.ButtonHref1,
                ButtonName2 = this.ButtonName2,
                EnglishButtonName2 = this.EnglishButtonName2,
                ButtonHref2 = this.ButtonHref2,
                Enable = this.Enable,
                Bg = this.BackgroundImage.Name
            };
        }
    }
}
