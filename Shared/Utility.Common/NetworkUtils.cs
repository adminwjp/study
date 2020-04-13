using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Utility
{
    /// <summary>
    /// 网络帮助公共类 
    /// </summary>
    public class NetworkUtils
    {
        /// <summary>
        /// 获取本地机器ip地址
        /// </summary>
        public static string LocalIp
        {
            get
            {
                // NetworkInterface.GetAllNetworkInterfaces();
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return string.Empty;
            }
        }
    }
}
