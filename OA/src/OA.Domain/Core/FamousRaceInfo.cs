using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "famous_race_info", Name = "FamousRaceInfo", NameType = typeof(FamousRaceInfo), Lazy = false)]
    public class FamousRaceInfo:BaseEntity
    {
        [Property(Column = "name", NotNull = true,  Length = 50)]
        public string Name { get; set; }
        [NHibernate.Mapping.Attributes.Drop]
        public int UserId { get; set; }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo))]
        public UserInfo User { get; set; }
    }
}
