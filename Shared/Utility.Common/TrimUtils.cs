using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility
{
    public class TrimUtils
    {
        public static string Trim(string str)
        {
            return Regex.Replace(str, "[\\s\r\n]+", "");
        }
    }
}
