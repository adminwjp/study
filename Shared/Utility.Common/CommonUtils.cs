#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
#if !NET20 && !NET30 && !NET35 && !NET40 && !NET45 && !NET451 && !NET452 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
using System.Threading.Tasks;
using System.Threading;
#endif


#if !NET20 && !NET30 && !NET35
using System.Linq;
using System.Xml.Linq;
#endif

namespace Utility
{
    /// <summary>
    /// 公共 类
    /// </summary>
    public static class CommonUtils
    {
      
       
        public static string GetEnumDesc(Type type,Enum obj,int val,Language language)
        {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            foreach (FieldInfo item in type.GetFields())
            {
                if ((int)item.GetValue(obj) == val)
                {
#if !NET20 && !NET30 && !NET35 && !NET40
                  var desc = item.GetCustomAttribute<DescAttribute>(false);
                    switch (language)
                    {
                      
                        case Language.English:
                            return desc.EnglishDesc;
                        case Language.Chinese:
                        default:
                            return desc.ChineseDesc;
                    }
#else
                    foreach (var attr in item.GetCustomAttributes(typeof(DescAttribute), false))
                    {
                        if (attr is DescAttribute attribute)
                        {
                            switch (language)
                            {
                                case Language.English:
                                    return attribute.EnglishDesc;
                                case Language.Chinese:
                                default:
                                    return attribute.ChineseDesc;
                            }
                        }
                    }
#endif
                 
                }
            }
#endif
            return string.Empty;
        }
        public static T GetEnumAttribute<T>(Type type,string filedName) where T : System.Attribute
        {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
#if !NET20 && !NET30 && !NET35 && !NET40
            return (T)type.GetField(filedName).GetCustomAttribute(typeof(T));
#else
            foreach (var attr in type.GetField(filedName).GetCustomAttributes(typeof(T), false))
            {
                if (attr is T attribute)
                {
                    return attribute;
                }
            }
            return default(T);
#endif
#else
            return default(T);
#endif
        }
        public static void CreateLinqXml(object obj,string fileName)
        {
#if  !(NET20 || NET30 || !NETSTANDARD1_0 || !NETSTANDARD1_1 || !NETSTANDARD1_2 || !NETSTANDARD1_3 || !NETSTANDARD1_4 || !NETSTANDARD1_5 || !NETSTANDARD1_6)
            XDocument document = new XDocument();
            Type type = obj.GetType();
            XElement element = new XElement(type.Name);
            document.Add(element);
            foreach (var item in type.GetFields())
            {
                element.Add(new XElement(item.Name,item.GetValue(obj)));
            }
            document.Save(fileName);
#endif
        }
  
        /// <summary>
        /// 字符串unicode转换
        /// <para>
        /// 不支持 netstandard 1.0 - 1.2
        /// </para>
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string Unicode2String(string source)
        {
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
#else
             return string.Empty;
#endif
        }
        /// <summary>
        /// guid string 
        /// </summary>
        public static string Id { get { return Guid.NewGuid().ToString("N"); } }

    }
}
#endif