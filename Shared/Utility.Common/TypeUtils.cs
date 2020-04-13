using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class TypeUtils
    {
        /// <summary>
        /// IsPrimitive为true，表示是.net的原生类型，即基础类型，注意string类型，自定义的struct  class不是原生类型
        /// <para>IsClass 表示是实体类</para>
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsCustomClass(Type type)
        {
            return !(type == null || type.IsPrimitive )&&type!=typeof(string)&& type.IsClass&&!type.IsAssignableFrom(typeof(ICollection<>));
        }
         public static IEnumerable<string> GetFields(Type type)
        {
            foreach (var item in type.GetFields())
            {
                yield return item.Name;
            }
        }
        public static IEnumerable<T> GetFieldsByEnum<T>(T obj)
        {
            foreach (var item in obj.GetType().GetFields())
            {
                yield return (T)item.GetValue(obj);
            }
        }
        public static IEnumerable<EnumEntity> GetFieldsEnum(object obj)
        {
            foreach (var item in obj.GetType().GetFields())
            {
                yield return new EnumEntity() { Name=item.Name,Value=(int)item.GetValue(obj)};
            }
        }
    }
    public class EnumEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
