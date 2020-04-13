using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("about_info")]
    public class AboutInfo:BaseDesc,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("background_image")]
        public ImageInfo BackgroundImage { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Bg { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("image")]
        public ImageInfo Image { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Img { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("title")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Title { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_title")]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        [BindProperty(Name = "english_title")]
        public string EnglishTitle { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }

        public object Clone()
        {
            return new AboutInfo()
            {
                Id = this.Id,
                Name = this.Name,
                EnglishName = this.EnglishName,
                Description = this.Description,
                EnglishDescription = this.EnglishDescription,
                CreateDate = this.CreateDate,
                ModifyDate = this.ModifyDate,
                Enable = this.Enable,
                Bg = this.BackgroundImage != null ? this.BackgroundImage.Name : null,
                Img = this.Image != null ? this.Image.Name : null,
                Title = this.Title,
                EnglishTitle = this.EnglishTitle
            };
        }
    }
}
