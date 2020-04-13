using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Domain.Core
{
    /// <summary>
    /// 我们的团队 关于我们
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("team_info")]
    public class TeamInfo:BaseName,ICloneable
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("img")]
        public ImageInfo Img { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("category_id")]
        public CategoryInfo Category { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("service_id")]
        public ServiceInfo Service { get; set; }
        public ICollection<TeamSourceInfo> TeamSources { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Source { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]

        public int[] Sources { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]

        public string Href { get; set; }
        public object Clone()
        {
            return new TeamInfo()
            {
                Id=this.Id,
                Name=this.Name,
                Category=new CategoryInfo() {
                    Id = this.Category.Id,
                    Name = this.Category.Name,
                    EnglishName = this.Category.EnglishName
                },
                EnglishName=this.EnglishName,
                Enable=this.Enable,
                CreateDate=this.CreateDate,
                ModifyDate=this.ModifyDate,
                Src=this.Img.Name,
                Service=new ServiceInfo()
                {
                    Id = this.Service.Id,
                    Name = this.Service.Name,
                    EnglishName = this.Service.EnglishName
                },
                Source=string.Join(",",this.TeamSources.Select(it=>it.Social.Icon))
            };
        }
    }
}
