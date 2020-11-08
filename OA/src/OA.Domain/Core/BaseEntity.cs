﻿using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace OA.Domain.Core
{
    public abstract class BaseEntity:IEntity<int>
    {
        private int _id;
        private DateTime? _createDate;
        private DateTime? _updateDate;
        [Id(Name ="Id",Column ="id",TypeType =typeof(int),UnsavedValue ="0")]
        [Generator(Class = "increment")]
        public virtual  int Id
        {
            get { return this._id; }
            set{ Set(ref _id,value, "Id"); }
        }
        [Property(Name = "CreateDate", Column  = "create_date", TypeType = typeof(DateTime))]
        public virtual DateTime? CreateDate
        {
            get { return this._createDate; }
            set { Set(ref _createDate, value, "CreateDate"); }
        }
        [Property(Name = "UpdateDate", Column = "update_date", TypeType = typeof(DateTime))]
        public virtual DateTime? UpdateDate
        {
            get { return this._updateDate; }
            set { Set(ref _updateDate, value, "UpdateDate"); }
        }
        protected virtual void Set<T>(ref T oldVal, T newVal, string propertyName = null)
        {
            oldVal = newVal;
        }

    }
}
