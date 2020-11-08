using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "famous_race_info", Name = "FamousRaceInfo", NameType = typeof(FamousRaceInfo), Lazy = false)]
    public class FamousRaceInfo:BaseEntity
    {
        private string _name;
        private int _userId;
        private UserInfo _user;
        [Property(Column = "name", NotNull = true,  Length = 50)]
        public string Name
        {
            get { return this._name; }
            set { Set(ref _name, value, "Name"); }
        }
        [NHibernate.Mapping.Attributes.Drop]
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
