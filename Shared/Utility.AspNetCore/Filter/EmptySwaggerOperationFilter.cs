using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SocialContact.Api.Filter
{
    public class EmptySwaggerOperationFilter : IOperationFilter,IDocumentFilter
    {
        public virtual void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
           
        }

        public virtual void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            
        }
    }
}
