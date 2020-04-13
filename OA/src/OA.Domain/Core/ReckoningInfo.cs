using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 账套信息
    /// </summary>
    [Class(Table = "reckoning_info", Lazy = false)]
    public class ReckoningInfo:BaseEntity
    {
        [ManyToOne(Column ="recore")]
        public RecordInfo Record { get; set; }
        [ManyToOne(Column = "account_item_id")]
        public AccountItemInfo AccountItem { get; set; }
        [Property(Column ="money")]
        public int Money { get; set; }
    }
}
