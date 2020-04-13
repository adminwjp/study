using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 奖惩信息
    /// </summary>
   [Class(Table = "reawrds_and_punishment_info", Lazy = false)]
    public class ReawrdsAndPunishmentInfo:BaseEntity
    {
        [ManyToOne(Column ="record_id")]
        public RecordInfo Record { get; set; }
        [Property(Column = "type")]
        public int Type { get; set; }
        [Property(Column = "reason",Length =500)]
        public string Reason { get; set; }
        [Property(Column = "content", Length = 200)]
        public string Content { get; set; }
        [Property(Column = "money")]
        public int Money { get; set; }
        [Property(Column = "start_date")]
        public DateTime StartDate { get; set; }
        [Property(Column = "end_date")]
        public DateTime EndDate { get; set; }
    }
}
