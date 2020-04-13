using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ReflectionUtils
    {
#if !(NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

        public static T Set<T>(dynamic data)where T : class
        {
            return default(T);
        }
#endif
        public static T Bind<T>(string[] propertyNames,object[] datas)
        {
            if (datas.Length == propertyNames.Length && propertyNames.Length>0)
            {
                Type type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    type.GetProperty(propertyNames[i]).SetValue(obj, datas[i]);
                }
                return obj;
            }
            return default(T);
        }
    }
}
