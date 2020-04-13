using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("category_info")]
    public class CategoryInfo:BaseDesc,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public BasicCategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("background_image")]
        public ImageInfo BackgroundImage { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Bg { get; set; }
        /// <summary>
        /// 最近作品
        /// </summary>
        
        public ICollection<WorkInfo> Works { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        /// <summary>
        /// 最近作品分类
        /// </summary>
        //public ICollection<WorkCategoryInfo> WorkCategories { get; set; }
        /// <summary>
        /// 我们的服务
        /// </summary>
        public ICollection<ServiceInfo> Services { get; set; }
        /// <summary>
        /// 我们的技能
        /// </summary>
        public ICollection<SkillInfo> Skills { get; set; }
        /// <summary>
        /// 菜单
        /// </summary>
        public ICollection<MenuInfo> Menus { get; set; }
        /// <summary>
        /// 感言
        /// </summary>
        public ICollection<TestimonialPersonInfo> Testimonials { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public ICollection<ThemeInfo> Themes { get; set; }
        /// <summary>
        /// 关于我们品牌
        /// </summary>
        public ICollection<BrandInfo> OurPartners { get; set; }
        /// <summary>
        /// 特征
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public ICollection<BrandInfo> Features { get; set; }
        /// <summary>
        /// 我们的团队
        /// </summary>
        public ICollection<TeamInfo> Teams { get; set; }
        public object Clone()
        {
            return new CategoryInfo()
            {
                Id = this.Id,
                Name = this.Name,
                EnglishName = this.EnglishName,
                Description = this.Description,
                EnglishDescription = this.EnglishDescription,
                Bg=this.BackgroundImage?.Name
            };
        }
    }
}
