using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "authority_operator_info", Name = "AuthorityOperatorInfo", NameType = typeof(AuthorityOperatorInfo), Lazy = false)]
    public class AuthorityOperatorInfo:BaseEntity
    {
        private string _table;
        private string _entityType;
        /// <summary>
        /// 1查、2改、3删、4增
        /// </summary>
        private int _addFalg;
        private int _editFalg;
        private int _deleteFalg;
        private int _selectFalg;
        private int _roleId;
        private RoleInfo _role;

        [Property( Column = "`table`", Length = 50)]
        public string Table
        {
            get { return this._table; }
            set { Set(ref _table, value, "Table"); }
        }
        [Property(Column = "entity_type", NotNull = true, Length = 50)]
        public string EntityType
        {
            get { return this._entityType; }
            set { Set(ref _entityType, value, "EntityType"); }
        }
        [Property( Column = "add_falg")]
        /// <summary>
        /// 1查、2改、3删、4增
        /// </summary>
        public int AddFalg
        {
            get { return this._addFalg; }
            set { Set(ref _addFalg, value, "AddFalg"); }
        }
        [Property( Column = "edit_falg")]	   
	    public int EditFalg
        {
            get { return this._editFalg; }
            set { Set(ref _editFalg, value, "EditFalg"); }
        }
        [Property( Column = "delete_falg")]
		public int DeleteFalg
        {
            get { return this._deleteFalg; }
            set { Set(ref _deleteFalg, value, "DeleteFalg"); }
        }
        [Property( Column = "selete_falg")]
		public int SelectFalg
        {
            get { return this._selectFalg; }
            set { Set(ref _selectFalg, value, "SelectFalg"); }
        }
        [NHibernate.Mapping.Attributes.Drop]
        public int RoleId
        {
            get { return this._roleId; }
            set { Set(ref _roleId, value, "RoleId"); }
        }
        [ManyToOne(Column = "role_id", ClassType = typeof(RoleInfo))]
        public virtual RoleInfo Role
        {
            get { return this._role; }
            set { Set(ref _role, value, "Role"); }
        }
    }
}
