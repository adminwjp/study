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
    public class ReckoningInfo : BaseEntity
    {
        private RecordInfo _record;
        private AccountItemInfo _accountItem;
        private int _money;
        [ManyToOne(Column = "recore")]
        public RecordInfo Record
        {
            get { return this._record; }
            set { Set(ref _record, value, "Record"); }
        }
        [ManyToOne(Column = "account_item_id")]
        public AccountItemInfo AccountItem
        {
            get { return this._accountItem; }
            set { Set(ref _accountItem, value, "AccountItem"); }
        }
        [Property(Column = "money")]
        public int Money
        {
            get { return this._money; }
            set { Set(ref _money, value, "Money"); }
        }
    }
}
