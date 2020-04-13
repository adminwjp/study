using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "role_info", NameType = typeof(RoleInfo), Lazy = false)]
    public class RoleInfo:BaseEntity
    {
        [Property(Column = "name", NotNull = true, TypeType = typeof(string), Length = 50)]
        public string Name { get; set; }
        public int ParentId { get; set; }
        [ManyToOne(2, ClassType = typeof(RoleInfo),Column = "parent_id")]
        public RoleInfo Parent { get; set; }
        [Bag( Table = "role_info",Lazy =CollectionLazy.False)]
        [Key(Column = "parent_id")]
        [OneToManyAttribute(2, ClassType = typeof(RoleInfo),NotFound = NotFoundMode.Ignore)]
        public ICollection<RoleInfo> Roles { get; set; }
        public int UserId { get; set; }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo))]
        public UserInfo User { get; set; }
    }
}
