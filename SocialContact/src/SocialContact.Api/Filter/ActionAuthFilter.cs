using System;
using SocialContact.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SocialContact.Api.Data;
using SocialContact.Api.Exceptions;
using Utility;

namespace SocialContact.Api.Filter
{
    public class ActionAuthFilter : IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var router = context.RouteData.Values;
            var controller = router["controller"].ToString().ToLower();
            if (controller.Equals("weatherforecast"))
            {
                return;
            }
            object controllerBase = context.Controller;
            Type type = controllerBase.GetType();
            var langeuage = (Language)type.GetMethod("GetLanguage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Default).Invoke(controllerBase, null);
            if (context.Exception is IdNotFoundException idNotFoundException)
            {
                ResponseApi response = ResponseApiUtils.GetResponse(langeuage,Utility.Code.DeleteFail);
                response.Message = idNotFoundException.Message;
                context.Result = new ObjectResult(response);
                context.ExceptionHandled = true;
                return;
            }
            if (context.Exception is AuthNotFoundException authNotFoundException)
            {
                ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.AuthFail);
                response.Message = authNotFoundException.Message;
                context.Result = new ObjectResult(response);
                context.ExceptionHandled = true;
                return;
            }
            if (context.Exception is CascadeException  cascadeException)
            {
                ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.CascadeDeleteFail);
                response.Message = cascadeException.Message;
                context.Result = new ObjectResult(response);
                context.ExceptionHandled = true;
                return;
            }
            if (context.Exception is Exception exception)
            {
                var loggerField = type.GetField("Logger", System.Reflection.BindingFlags.NonPublic
                    | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance);
                var logger = (ILogger)loggerField.GetValue(controllerBase);
                var action = router["action"].ToString().ToLower();
                logger.LogDebug($"controller {controller} action {action} 执行异常：{exception.Message} {exception.StackTrace} ");
                ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.Error);
                response.Message = "系统繁忙!" + exception.Message + exception.StackTrace;
                context.Result = new ObjectResult(response);
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var router = context.RouteData.Values;
            var controller = router["controller"].ToString().ToLower();
            var action = router["action"].ToString().ToLower();
            if (controller.Equals("weatherforecast"))
            {
                return;
            }
            if(((controller.Equals("admin") || controller.Equals("user")) && action.Equals("login"))||
                controller.Equals("img")||(controller.Equals("file")&&action.Equals("get")))
            {
                return;
            }
            if (!controller.Equals("admin")|| controller.Equals("admin")) return;
            object controllerBase = context.Controller;
            Type type = controllerBase.GetType();
            var redisField = type.GetField("RedisCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.Instance);
            var redisCache = (RedisCache)redisField.GetValue(controllerBase);
            //var unitWorkField = type.GetField("UnitWork", System.Reflection.BindingFlags.NonPublic
            // | System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.Instance);
            // var unitWork = (IUnitWork)unitWorkField.GetValue(controllerBase);
            var cacheField = type.GetField("Cache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField
           | System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.Instance);
            var cache = (IMemoryCache)cacheField.GetValue(controllerBase);
            string token = ((ControllerBase)controllerBase).HttpContext.Request.Query["token"];
            var langeuage = (Language)type.GetMethod("GetLanguage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Default).Invoke(controllerBase,null) ;
            if (string.IsNullOrEmpty(token))
            {
                token = ((ControllerBase)controllerBase).HttpContext.Request.Headers["token"];
                //if (!string.IsNullOrEmpty(token)) token=token.AesDecrypt(Core.AesKey, Core.AesIv);
            }
            if (string.IsNullOrEmpty(token))
            {
                token = ((ControllerBase)controllerBase).HttpContext.Request.Cookies["token"];
            }
            if (string.IsNullOrEmpty(token))
            {
                ResponseApi response = ResponseApiUtils.GetResponse(langeuage,Utility.Code.TokenNotNull);
                context.Result = new JsonResult(response);
                return;
            }
            //string key = ((ControllerBase)controllerBase).HttpContext.Request.Headers["key"];
            //if (string.IsNullOrEmpty(key))
            //{
            //    ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.AuthFail);
            //    context.Result = new JsonResult(response);
            //    return;
            //}
           // string value = key.AesDecrypt(Core.AesKey, Core.AesIv);
            //string account = value.Split('_')[0];
            var data = cache.Get<AdminInfo>(token);
            if (data == null)
            {
                var tokenStr = redisCache.GetString(token);
                if (string.IsNullOrEmpty(tokenStr))
                {
                    ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.AuthFail);
                    context.Result = new JsonResult(response);
                    return;
                }
                data = tokenStr.ToObject<AdminInfo>();
                if (data.LoginDate.Value.AddHours(24) < DateTime.Now.AddMinutes(-5))
                {
                    ResponseApi response = ResponseApiUtils.GetResponse(langeuage, Utility.Code.TokenExpires);
                    context.Result = new JsonResult(response);
                    return;
                }
                cache.Set<AdminInfo>(token, data, data.LoginDate.Value.AddHours(24));
            }
        }
    }
}
