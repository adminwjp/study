using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Utility.AspNetCore.Filter
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
#if NETCOREAPP3_0 || NETCOREAPP3_1
        private readonly IWebHostEnvironment _env;
        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this._env = env;
            this._logger = logger;
        }
#else
        private readonly IHostingEnvironment _env;
        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this._env = env;
            this._logger = logger;
        }
#endif
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.HResult==400)
            {
                context.Result = new BadRequestObjectResult(new Utility.ResponseApi()) { StatusCode = StatusCodes.Status400BadRequest };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var result=new Utility.ResponseApi<Exception>();
                if (_env.IsDevelopment())
                {
                    result.Data = context.Exception;
                }

                context.Result = new ObjectResult(result) { StatusCode = StatusCodes.Status500InternalServerError };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
