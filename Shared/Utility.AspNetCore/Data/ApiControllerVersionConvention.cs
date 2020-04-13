using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utility.AspNetCore.Data
{
    public class ApiControllerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (!(controller.ControllerType.IsDefined(typeof(ApiVersionAttribute)) || controller.ControllerType.IsDefined(typeof(ApiVersionNeutralAttribute))))
            {
                if (controller.Attributes is List<object>
                    attributes)
                {
                    attributes.Add(new ApiVersionNeutralAttribute());
                }
            }
        }
    }
}
