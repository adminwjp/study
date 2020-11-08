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
        private BringUpContentInfo _bringUpContent;
        private RecordInfo _record;
        private int _score;
        /// <summary>
        /// 培训等级
        /// </summary>
        private string _upToGrate;
        private string _remark;
        [ManyToOne(2, ClassType = typeof(BringUpContentInfo), Column = "bring_up_content_id")]
        public virtual BringUpContentInfo BringUpContent
        {
            get { return this._bringUpContent; }
            set { Set(ref _bringUpContent, value, "BringUpContent"); }
        }
        [ManyToOne(2, ClassType = typeof(RecordInfo), Column = "record_id")]
        public virtual RecordInfo Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [Property(Column = "score", TypeType = typeof(int))]
        public int Score
        {
            get { return this._score; }
            set { Set(ref _score, value, "Score"); }
        }
        /// <summary>
        /// 培训等级
        /// </summary>
        [Property(Column = "up_to_grate",  Length = 2)]
        public string UpToGrate
        {
            get { return this._upToGrate; }
            set { Set(ref _upToGrate, value, "UpToGrate"); }
        }
        [Property(Column = "remark", NotNull = true, Length = 200)]
        public string Remark
        {
            get { return this._remark; }
            set { Set(ref _remark, value, "Remark"); }
        }
    }
}
