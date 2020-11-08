using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.AspNetCore;
using Utility.IO;

namespace OA.Api
{
    public class Program
    {
       public readonly  static string UploadImg = Environment.CurrentDirectory + "\\imgs";
        public static void Main(string[] args)
        {
            FileHelper.CreateDirectory(UploadImg);
            StartHelper.Start<Startup>("OA Api",args);
        }
    }
}
