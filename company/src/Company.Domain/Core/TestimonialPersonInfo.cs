using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("testimonial_person_info")]
    public class TestimonialPersonInfo: BaseDesc,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("review")]
        public int Review { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("person_pic")]
        [BindProperty(Name = "person_pic")]
        public ImageInfo PersonPic { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Person { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("testimonial_id")]
        public CategoryInfo Category { get; set; }
       // [System.ComponentModel.DataAnnotations.Schema.Column("testimonial_id")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //属性名称跟name不一致则创建新的列名
        [FromForm(Name = "testimonial_id")]
        public int? TestimonialId{ get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }

        public object Clone()
        {
            return new TestimonialPersonInfo()
            {
                Review=this.Review,
                Name=this.Name,
                EnglishName = this.EnglishName,
                Description = this.Description,
                EnglishDescription = this.EnglishDescription,
                Person=this.PersonPic?.Name,
                Category=this.Category
            };
        }
    }
}
