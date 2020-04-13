using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.UI
{
    public class UrlMiddleware
    {
        private readonly RequestDelegate _next;

        public UrlMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var option = httpContext.Request.Path.Value;
            var str = Regex.Replace(httpContext.Request.Path.Value, "/admin/(.*)", "/src/admin/$1");
            Console.WriteLine("UrlMiddleware.Invoke:{0} {1}", option, str);
            str = Regex.Replace(str, "/company/(.*)", "/src/ui/$1");
            Console.WriteLine("UrlMiddleware.Invoke:{0} ", str);
            str = Regex.Replace(str, "/js/index.js", "/src/js/index.js");
            Console.WriteLine("UrlMiddleware.Invoke:{0} ", str);
            httpContext.Request.Path = new PathString(str);
            await _next(httpContext);
        }
    }
}
