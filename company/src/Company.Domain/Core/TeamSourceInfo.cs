using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    /// <summary>
    /// 关于我们 团队 来源
    /// </summary>
    [System.ComponentModel.DataAnnotations.Schema.Table("team_source_info")]
    public class TeamSourceInfo:BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("team_id")]
        public TeamInfo Team { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("social_id")]
        public SocialInfo Social { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
