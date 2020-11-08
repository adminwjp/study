﻿using NHibernate.Mapping.Attributes;
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
        private RecordInfo _record;
        private UserInfo _user;
        private string _explain;//迟到原因
        private DateTime _startDate;//上班时间
        private DateTime _endDate;//下班时间
        private RecordInfo _ratifierRecord;//批准人
        private DateTime _ratifierDate;

        [ManyToOne(Column ="record_id")]
        public RecordInfo Record  
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [ManyToOne(Column = "user_id",Lazy = Laziness.Unspecified)]
        public UserInfo User
        {
            get { return this._user; }
            set { Set(ref _user, value, "User"); }
        }
        /// <summary>
        /// 迟到原因
        /// </summary>
        [Property(Column = "`explain`",Length =200)]
        public string Explain
        {
            get { return this._explain; }
            set { Set(ref _explain, value, "Explain"); }
        }
        /// <summary>
        /// 上班时间
        /// </summary>
        [Property(Column = "start_date")]
        public DateTime StartDate
        {
            get { return this._startDate; }
            set { Set(ref _startDate, value, "StartDate"); }
        }
        /// <summary>
        /// 下班时间
        /// </summary>
        [Property(Column = "end_date")]
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { Set(ref _endDate, value, "EndDate"); }
        }
        /// <summary>
        /// 批准人
        /// </summary>
        [ManyToOne(Column = "ratifier_record_id")]
        public RecordInfo RatifierRecord
        {
            get { return this._ratifierRecord; }
            set { Set(ref _ratifierRecord, value, "RatifierRecord"); }
        }
        [Property(Column = "ratifier_date")]
        public DateTime RatifierDate
        {
            get { return this._ratifierDate; }
            set { Set(ref _ratifierDate, value, "RatifierDate"); }
        }
    }
}
