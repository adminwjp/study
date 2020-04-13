using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    ///账套 人员设置
    /// </summary>
    [Class(Table = "reckoning_list_info", NameType = typeof(ReckoningListInfo), Lazy = false)]
    public class ReckoningListInfo:BaseEntity
    {
        [ManyToOne(Column ="record_id")]
        public RecordInfo Record { get; set; }
        [ManyToOne(Column = "reckoning_name_id")]
        public ReckoningNameInfo ReckoningName { get; set; }
    }
}
