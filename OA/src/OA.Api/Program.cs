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

namespace OA.Api
{
    public class Program
    {
       public readonly  static string UploadImg = Environment.CurrentDirectory + "\\imgs";
        public static void Main(string[] args)
        {
            FileUtils.CreateDirectory(UploadImg);
            StartUtils.Init<Startup>("OA Api",args);
        }
    }
}
