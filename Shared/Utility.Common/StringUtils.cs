using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class StringUtils
    {
        /// <summary>
        /// 是否是char 字符
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsChar(Type type)
        {
            return type == typeof(char) || type == typeof(char?);
        }
        /// <summary>
        ///字符转换
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static  string Parse(string str, StringFormat format)
        {
            switch (format)
            {
                case StringFormat.Lower:
                    return str.ToLower();
                case StringFormat.Upper:
                    return str.ToUpper();
                case StringFormat.InitialLetterUpperCaseLower:
                    {
                        StringBuilder builder = new StringBuilder(str.Length + 10);
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (i == 0)
                                builder.Append(char.ToLower(str[i]));
                            else if (char.IsUpper(str[i]))
                            {
                                builder.Append("_").Append(char.ToLower(str[i]));
                            }
                            else
                            {
                                builder.Append(str[i]);
                            }
                        }
                        str = builder.ToString();
                        return str;
                    }
                case StringFormat.InitialLetterLowerCaseUpper:
                    {
                        StringBuilder builder = new StringBuilder(str.Length);
                        for (int i = 0; i < str.Length;)
                        {
                            if (i == 0)
                                builder.Append(char.ToUpper(str[i]));
                            else if (str[i] == '_')
                            {
                                i++;
                                builder.Append(char.ToUpper(str[i]));
                            }
                            else
                            {
                                builder.Append(str[i]);
                            }
                            i++;
                        }
                        str = builder.ToString();
                        return str;
                    }
                default:
                    return str;
            }
         
        }
        public static StringFormat Get(StringFormat stringFormat)
        {
            StringFormat format = StringFormat.None;
            switch (stringFormat)
            {
                case StringFormat.Lower:
                    format = StringFormat.Upper;
                    break;
                case StringFormat.Upper:
                    format = StringFormat.Lower;
                    break;
                case StringFormat.InitialLetterUpperCaseLower:
                    format = StringFormat.InitialLetterLowerCaseUpper;
                    break;
                case StringFormat.InitialLetterLowerCaseUpper:
                    format = StringFormat.InitialLetterUpperCaseLower;
                    break;
                default:
                    break;
            }
            return format;
        }
        /// <summary>
        /// 获取字符串不同之处的位置
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        public static int GetDiffIndex(string str1,string str2)
        {
            if (str2.Length > str1.Length)
            {
                return str1.Length;
            }
            for (int i = 0; i <str1.Length&&i<str2.Length; i++)
            {
                if (!str1[i].Equals(str2[i]) )
                {
                    return i;
                }
            }
            return -1;
        }
    }
    public enum StringFormat
    {
        /// <summary>
        /// 默认字符 aBc aBc
        /// </summary>
        None,
        /// <summary>
        /// 小写 aBc abc
        /// </summary>
        Lower,
        /// <summary>
        /// 大写 aBc ABC
        /// </summary>
        Upper,
        /// <summary>
        /// 首字母大写转小写 其他大写字母转横线加小写字母 aBc a_bc
        /// </summary>
        InitialLetterUpperCaseLower,
        /// <summary>
        ///  首字母小写转 大写 其他横线加小写字母转大写字母  a_bc aBc
        /// </summary>
        InitialLetterLowerCaseUpper
    }
}
