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
        [Property(Name = "Name", Column  = "name", NotNull = true, TypeType = typeof(string),Length =50)]
        public string Name { get; set; }
        [Property(Column = "href", TypeType = typeof(string),Length =100)]
        public string Href{ get; set; }
        public int ParentId { get; set; }
	    [ManyToOne(Column = "parent_id", ClassType = typeof(ModuleInfo),Lazy = Laziness.Unspecified)]
		public ModuleInfo Parent { get; set; }
        [Bag(0, Table = "module_info",Lazy = CollectionLazy.False)]
        [Key(1, Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(ModuleInfo))]
        public IList<ModuleInfo> Modules { get; set; }
        public int UserId { get; set; }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo), Lazy = Laziness.Unspecified)]
        public UserInfo User { get; set; }
    }
}
