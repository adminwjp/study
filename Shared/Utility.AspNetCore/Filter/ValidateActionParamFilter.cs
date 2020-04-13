using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.AspNetCore.Extensions;

namespace Utility.AspNetCore.Filter
{
    public class ValidateActionParamFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ResponseApi responseApi = ResponseApiUtils.GetResponse(Language.Chinese, Utility.Code.ParamError);
                responseApi.Data = context.ModelState.Errors();
                context.Result = new JsonResult(responseApi) { StatusCode = 400 };
            }
        }
    }
}
