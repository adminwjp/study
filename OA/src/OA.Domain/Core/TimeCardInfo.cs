using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 考勤信息
    /// </summary>
    [Class(Table = "time_card_nfo", Lazy = false)]
    public class TimeCardInfo:BaseEntity
    {
        [ManyToOne(Column ="record_id")]
        public RecordInfo Record { get; set; }
        [ManyToOne(Column = "user_id",Lazy = Laziness.Unspecified)]
        public UserInfo User { get; set; }
        /// <summary>
        /// 迟到原因
        /// </summary>
        [Property(Column = "`explain`",Length =200)]
        public string Explain { get; set; }
        /// <summary>
        /// 上班时间
        /// </summary>
        [Property(Column = "start_date")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 下班时间
        /// </summary>
        [Property(Column = "end_date")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 批准人
        /// </summary>
        [ManyToOne(Column = "ratifier_record_id")]
        public RecordInfo RatifierRecord { get; set; }
        [Property(Column = "ratifier_date")]
        public DateTime RatifierDate { get; set; }
    }
}
