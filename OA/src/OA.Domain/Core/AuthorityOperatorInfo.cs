using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "authority_operator_info", Name = "AuthorityOperatorInfo", NameType = typeof(AuthorityOperatorInfo), Lazy = false)]
    public class AuthorityOperatorInfo:BaseEntity
    {
        [Property( Column = "`table`", Length = 50)]
        public string Table { get; set; }
        [Property(Column = "entity_type", NotNull = true, Length = 50)]
        public string EntityType { get; set; }
        [Property( Column = "add_falg")]
        /// <summary>
        /// 1查、2改、3删、4增
        /// </summary>
        public int AddFalg { get; set; }
        [Property( Column = "edit_falg")]	   
	    public int EditFalg { get; set; }
	    [Property( Column = "delete_falg")]
		public int DeleteFalg { get; set; }
	    [Property( Column = "selete_falg")]
		public int SelectFalg { get; set; }
        [NHibernate.Mapping.Attributes.Drop]
        public int RoleId { get; set; }
        [ManyToOne(Column = "role_id", ClassType = typeof(RoleInfo))]
        public RoleInfo Role { get; set; }
    }
}
