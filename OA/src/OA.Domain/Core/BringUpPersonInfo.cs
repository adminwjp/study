using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 培训人员信息
    /// </summary>
    [Class(Table = "bring_up_person_info", Name = "BringUpPersonInfo", NameType = typeof(BringUpPersonInfo), Lazy = false)]
    public class BringUpPersonInfo : BaseEntity
    {
        [ManyToOne(2, ClassType = typeof(BringUpContentInfo), Column = "bring_up_content_id")]
        public BringUpContentInfo BringUpContent { get; set; }
        [ManyToOne(2, ClassType = typeof(RecordInfo), Column = "record_id")]
        public RecordInfo Record { get; set; }
        [Property(Column = "score", TypeType = typeof(int))]
        public int Score { get; set; }
        /// <summary>
        /// 培训等级
        /// </summary>
        [Property(Column = "up_to_grate",  Length = 2)]
        public string UpToGrate { get; set; }
        [Property(Column = "remark", NotNull = true, Length = 200)]
        public string Remark { get; set; }
    }
}
