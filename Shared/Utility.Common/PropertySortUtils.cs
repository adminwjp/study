using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class PropertySortUtils
    {
        /// <summary>
        /// 获取插入的位置 有基类则从0开始 自增 否则原有位置上自增 
        /// </summary>
        /// <param name="j">索引</param>
        /// <param name="type">类型</param>
        /// <param name="name">属性名称</param>
        /// <returns>返回插入位置 索引</returns>
        public static int Cursions(int j, Type type, string name)
        {
            if (type.BaseType != null && type.BaseType.GetProperty(name) != null)
            {
                j++;
                var temp = Cursions(-1, type.BaseType, name);
                if (temp == -1)
                    return j;
            }
            return -1;
        }
    }
}
