using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 考勤/账套项目
    /// </summary>
    [Class(Table = "account_item_info", NameType = typeof(AccountItemInfo), Lazy = false)]
    public class AccountItemInfo:BaseEntity
    {
        [Property(Column = "name", Length = 20)]
        public string Name { get; set; }
        [Property(Column = "type", Length = 4)]
        ///1 考勤 / 2 账套
        public string Type { get; set; }
        [Property(Column = "utit", Length = 20)]
        public string Utit { get; set; }
    }
}
