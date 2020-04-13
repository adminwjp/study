using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("work_info")]
    public class WorkInfo:BaseInfo,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("img_id")]
        public ImageInfo Image { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }

        public object Clone()
        {
            return new WorkInfo()
            {
                Id=this.Id,
                CreateDate=this.CreateDate,
                Enable=this.Enable,
                ModifyDate=this.ModifyDate,
                Src=this.Image.Name,
                Category=new CategoryInfo()
                {
                    Id = this.Category.Id,
                    Name = this.Category.Name,
                    EnglishName = this.Category.EnglishName
                }
            };
        }
    }
}
