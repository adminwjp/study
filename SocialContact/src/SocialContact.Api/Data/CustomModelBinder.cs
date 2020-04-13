using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Data
{
    public class CustomModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelKindName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, "file_category");
            var modelTypeValue = bindingContext.ValueProvider.GetValue("file_category").FirstValue;
           
            IModelBinder modelBinder=null;
            ModelMetadata modelMetadata=null;
            string key = bindingContext.ModelName;

            if (String.IsNullOrEmpty(key))
            {
                key = bindingContext.FieldName;
            }


            string val = bindingContext.HttpContext.Request.Form[key];

            if (String.IsNullOrEmpty(val))
            {
                val = bindingContext.HttpContext.Request.Query[key];
            }

            if (String.IsNullOrEmpty(val))
            {
                 await   Task.CompletedTask;
            }

            if (bindingContext.ModelType == typeof(string))
            {
                bindingContext.Model = val;
            }

            if (bindingContext.ModelType == typeof(int))
            {
                bindingContext.Model = int.Parse(val);
            }

            if (bindingContext.ModelType == typeof(long))
            {
                bindingContext.Model = long.Parse(val);
            }

            if (bindingContext.ModelType == typeof(float))
            {
                bindingContext.Model = float.Parse(val);
            }

            if (bindingContext.ModelType == typeof(double))
            {
                bindingContext.Model = double.Parse(val);
            }

            if (bindingContext.ModelType == typeof(short))
            {
                bindingContext.Model = short.Parse(val);
            }

            if (bindingContext.ModelType == typeof(DateTime))
            {
                bindingContext.Model = DateTime.Parse(val);
            }

            if (bindingContext.Model != null)
            {
                bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            }

            if (bindingContext.ModelType == typeof(IFormFile))
            {
                bindingContext.Model = bindingContext.HttpContext.Request.Form.Files[0];
            }
            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
           bindingContext.ActionContext,
           bindingContext.ValueProvider,
           modelMetadata,
           bindingInfo: null,
           bindingContext.ModelName);

            await modelBinder.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;

            if (newBindingContext.Result.IsModelSet)
            {
                // Setting the ValidationState ensures properties on derived types are correctly 
                bindingContext.ValidationState[newBindingContext.Result] = new ValidationStateEntry
                {
                    Metadata = modelMetadata,
                };
            }
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);

             await Task.CompletedTask;
        }
        
    }
}
