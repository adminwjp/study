using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class PageUtils
    {
        public static int MaxPage = 999;
        public static int MaxSize = 200;
        public static int MinSize = 10;
        public static void Set(ref int? page,ref int? size)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            else if (page > 1000)
            {
                page = PageUtils.MaxPage;
            }
            if (size == null || size < 10)
            {
                size = PageUtils.MinSize;
            }
            else if (size > 200)
            {
                size = PageUtils.MaxSize;
            }
        }
        public static void Set(ref int page, ref int size)
        {
            if (page < 1)
            {
                page = 1;
            }
            else if (page > 1000)
            {
                page = PageUtils.MaxPage;
            }
            if (size < 10)
            {
                size = PageUtils.MinSize;
            }
            else if (size > 200)
            {
                size = PageUtils.MaxSize;
            }
        }
    }
}
