using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    public abstract class BaseEntity
    { 
        [Id(Name ="Id",Column ="id",TypeType =typeof(int),UnsavedValue ="0")]
        [Generator(Class = "increment")]
        public virtual  int Id { get; set; }
        [Property(Name = "CreateDate", Column  = "create_date", TypeType = typeof(DateTime))]
        public virtual DateTime? CreateDate { get; set; }
        [Property(Name = "UpdateDate", Column = "update_date", TypeType = typeof(DateTime))]
        public virtual DateTime? UpdateDate { get; set; }

    }
}
