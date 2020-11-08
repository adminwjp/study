using NHibernate.Engine;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "module_info", Name = "ModuleInfo", NameType = typeof(ModuleInfo), Lazy = false)]
    public class ModuleInfo:BaseEntity
    {
        private string _name;
        private string _href;
        private int _parentId;
        private ModuleInfo _parent;
        private IList<ModuleInfo> _modules;
        private int  _userId;
        private UserInfo _user;
        [Property(Name = "Name", Column  = "name", NotNull = true, TypeType = typeof(string),Length =50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [Property(Column = "href", TypeType = typeof(string),Length =100)]
        public string Href
        {
            get { return this._href; }
            set { Set(ref _href, value, "Href"); }
        }
        public int ParentId
        {
            get { return this._parentId; }
            set { Set(ref _parentId, value, "ParentId"); }
        }
        [ManyToOne(Column = "parent_id", ClassType = typeof(ModuleInfo),Lazy = Laziness.Unspecified)]
		public ModuleInfo Parent
        {
            get { return this._parent; }
            set { Set(ref _parent, value, "Parent"); }
        }
        [Bag(0, Table = "module_info",Lazy = CollectionLazy.False)]
        [Key(1, Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(ModuleInfo))]
        public IList<ModuleInfo> Modules
        {
            get { return this._modules; }
            set { Set(ref _modules, value, "Modules"); }
        }
        public int UserId 
        {
            get { return this._userId; }
            set { Set(ref _userId, value, "UserId"); }
        }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo), Lazy = Laziness.Unspecified)]
        public UserInfo User
        {
            get { return this._user; }
            set { Set(ref _user, value, "User"); }
        }
    }
}
