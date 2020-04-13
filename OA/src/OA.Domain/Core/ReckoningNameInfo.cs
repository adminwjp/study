using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 账套 名称 信息
    /// </summary>
    [Class(Table = "reckoning_name_info", Lazy = false)]
    public class ReckoningNameInfo:BaseEntity
    {
        [Property(Column = "name", Length = 20)]
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Property(Column = "`explain`", Length = 200)]
        public string Explain { get; set; }
    }
}
