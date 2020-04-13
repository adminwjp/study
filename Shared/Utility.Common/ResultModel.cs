using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ResultModel<T>
    {
        public List<T> Data { get; set; }
        public PageModel Result { get; set; }
    }
    public class PageModel
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
    }
}
