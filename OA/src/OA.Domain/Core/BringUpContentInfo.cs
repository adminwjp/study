using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 培训内容信息
    /// </summary>
    [Class(Table = "bring_up_content_info", Name = "BringUpContentInfo", NameType = typeof(BringUpContentInfo), Lazy = false)]
    public class BringUpContentInfo:BaseEntity
    {
        [Property( Column = "name",  Length = 50)]
        public string Name { get; set; }
        [Property(Column = "content", Length = 100)]
        public string Content { get; set; }
        [Property(Column = "start_date")]
        public DateTime StartDate { get; set; }
        [Property(Column = "end_date")]
        public DateTime EndDate { get; set; }
        [Property(Column = "unit", Length = 50)]
        public string Unit { get; set; }
        /// <summary>
        /// 讲课者
        /// </summary>
        [Property( Column = "lecturer",  Length = 50)]
        public string Lecturer { get; set; }
        [Property(Column = "place", Length = 100)]
        public string Place { get; set; }
    }
}
