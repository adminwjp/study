using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ObjectEntry
    {
        public object Object { get; set; }
        public ObjectState State { get; set; }
    }
    public class ObjectEntry<T> : ObjectEntry
    {
        public new T Object { get; set; }
    }
}
