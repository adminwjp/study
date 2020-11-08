using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "role_info", NameType = typeof(RoleInfo), Lazy = false)]
    public class RoleInfo:BaseEntity
    {
        private string _name;
        private  int _parentId;
        private RoleInfo _parent;
        private ICollection<RoleInfo> _roles;
        private int _userId;
        private UserInfo _user;
        [Property(Column = "name", NotNull = true, TypeType = typeof(string), Length = 50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        public int ParentId
        {
            get { return this._parentId; }
            set { Set(ref _parentId, value, "ParentId"); }
        }
        [ManyToOne(2, ClassType = typeof(RoleInfo),Column = "parent_id")]
        public RoleInfo Parent
        {
            get { return this._parent; }
            set { Set(ref _parent, value, "Parent"); }
        }
        [Bag( Table = "role_info",Lazy =CollectionLazy.False)]
        [Key(Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(RoleInfo),NotFound = NotFoundMode.Ignore)]
        public ICollection<RoleInfo> Roles
        {
            get { return this._roles; }
            set { Set(ref _roles, value, "Roles"); }
        }
        public int UserId
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo))]
        public UserInfo User
        {
            get { return this._user; }
            set { Set(ref _user, value, "User"); }
        }
    }
}
