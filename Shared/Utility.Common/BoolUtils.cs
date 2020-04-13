using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class BoolUtils
    {
        public static bool IsBoolean(Type type)
        {
            return type == typeof(bool) || type == typeof(bool?);
        }
    }
}
