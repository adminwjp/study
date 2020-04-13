#if !(NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public interface IPlugin
    {
        string Name { get; set; }
        bool Enable { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        string Url { get; set; }
        ResponseApi Collect(Hashtable param);
    }
}
#endif