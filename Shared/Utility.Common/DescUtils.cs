#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility
{
    public class DescUtils
    {
        public static readonly Dictionary<string, DescAttribute> DescAttributes = new Dictionary<string, DescAttribute>();
        public static void InitCode()
        {
            foreach (var item in typeof(Code).GetFields())
            {
#if !(NET20 || NET30 || NET35 || NET40)
                var desc = item.GetCustomAttribute<DescAttribute>();
#else
                var desc=AttributeUtils.Get<DescAttribute>(item.GetCustomAttributes(true));
#endif
                if(desc != null)
                {
                    DescAttributes.Add(item.GetValue(Code.Success).ToString(),desc);
                }
            }
        }
        public static DescAttribute GetDescAttribute(object val)
        {
            ArgumentsUtils.CheckArgumentObjectNull("obj", val);
            int code = (int)val;
            foreach (var item in val.GetType().GetFields())
            {
                object res = item.GetValue(val);
                if ((int)res == code)
                {
#if !(NET20 || NET30 || NET35 || NET40)
                    var desc = item.GetCustomAttribute<DescAttribute>();
#else
                var desc=AttributeUtils.Get<DescAttribute>(item.GetCustomAttributes(true));
#endif
                    if (desc == null) continue;
                    return desc;
                }
            }
            return (DescAttribute)null;
        }
    }
}
#endif