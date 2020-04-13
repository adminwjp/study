using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility
{
    public class AttributeUtils
    {
        public static T Get<T>(object[] objs)where T : Attribute
        {
            foreach (var item in objs)
            {
                if (item is T) return (T)item;
            }
            return (T)null;
        }
        public static bool Exists<T>(object[] objs) where T : Attribute
        {
            if (Get<T>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T,T1>(object[] objs) where T : Attribute where T1 :Attribute
        {
            if (Get<T>(objs) != null)
            {
                return true;
            }
            if (Get<T1>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1,T2>(object[] objs) where T : Attribute where T1 : Attribute where T2 : Attribute
        {
            if (Exists<T,T1>(objs))
            {
                return true;
            }
            if (Get<T2>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2,T3>(object[] objs) where T : Attribute where T1 : Attribute where T2 : Attribute where T3 : Attribute
        {
            if (Exists<T,T1,T2>(objs))
            {
                return true;
            }
            if (Get<T3>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3,T4>(object[] objs) where T : Attribute where T1 : Attribute 
            where T2 : Attribute where T3 : Attribute where T4 : Attribute
        {
            if (Exists<T,T1,T2,T3>(objs))
            {
                return true;
            }
            if (Get<T4>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3, T4,T5>(object[] objs) where T : Attribute where T1 : Attribute
    where T2 : Attribute where T3 : Attribute where T4 : Attribute where T5 : Attribute
        {
            if (Exists<T, T1, T2, T3,T4>(objs))
            {
                return true;
            }
            if (Get<T5>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3, T4, T5,T6>(object[] objs) where T : Attribute where T1 : Attribute
where T2 : Attribute where T3 : Attribute where T4 : Attribute where T5 : Attribute where T6 : Attribute
        {
            if (Exists<T, T1, T2, T3, T4,T5>(objs))
            {
                return true;
            }
            if (Get<T6>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3, T4, T5, T6,T7>(object[] objs) where T : Attribute where T1 : Attribute
where T2 : Attribute where T3 : Attribute where T4 : Attribute where T5 : Attribute where T6 : Attribute where T7 : Attribute
        {
            if (Exists<T, T1, T2, T3, T4, T5,T6>(objs))
            {
                return true;
            }
            if (Get<T7>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3, T4, T5, T6, T7,T8>(object[] objs) where T : Attribute where T1 : Attribute
where T2 : Attribute where T3 : Attribute where T4 : Attribute where T5 : Attribute where T6 : Attribute where T7 : Attribute
             where T8 : Attribute
        {
            if (Exists<T, T1, T2, T3, T4, T5, T6,T7>(objs))
            {
                return true;
            }
            if (Get<T8>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static bool Exists<T, T1, T2, T3, T4, T5, T6, T7, T8,T9>(object[] objs) where T : Attribute where T1 : Attribute
where T2 : Attribute where T3 : Attribute where T4 : Attribute where T5 : Attribute where T6 : Attribute where T7 : Attribute
     where T8 : Attribute where T9 : Attribute
        {
            if (Exists<T, T1, T2, T3, T4, T5, T6, T7,T8>(objs))
            {
                return true;
            }
            if (Get<T9>(objs) != null)
            {
                return true;
            }
            return false;
        }
        public static IEnumerable<T> GetEnumAttribute<T>(Type type) where T : System.Attribute
        {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            foreach (var item in type.GetFields())
            {
#if !NET20 && !NET30 && !NET35 && !NET40
                T attribute = (T)item.GetCustomAttribute(typeof(T));
                if (attribute != null) yield return attribute;
#else
                foreach (var attr in item.GetCustomAttributes(typeof(T), false))
                {
                    if (attr is T attribute)
                    {
                        yield return attribute;
                    }
                }
#endif

            }
#else
            return (IEnumerable<T>)null;
#endif
        }
    }
}
