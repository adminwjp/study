using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    /// <summary>
    /// 关于我们品牌
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("brand_info")]
    public  class BrandInfo:BaseDesc,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("logo")]
        public ImageInfo Logo { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("href")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Href { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
        /// <summary>
        /// 特征
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("feature")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Feature { get; set; }
        public object Clone(ImageInfo logo,CategoryInfo category)
        {
            this.Logo = logo;
            this.Category = category;
            return this.Clone();
        }
        public object Clone()
        {
            return new BrandInfo()
            {
                Id = this.Id,
                Name = this.Name,
                EnglishName = this.EnglishName,
                Description = this.Description,
                EnglishDescription = this.EnglishDescription,
                CreateDate=this.CreateDate,
                ModifyDate=this.ModifyDate,
                Enable=this.Enable,
                Href=this.Href,
                Feature=this.Feature,
                Src=this.Logo?.Name,
                Category = this.Category.Clone() as CategoryInfo,

            };
        }
    }
}
