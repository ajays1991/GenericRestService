using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Collections.Generic;

using GenericRestService.Controllers;

namespace GenericRestService.ControllerFactory
{
    public class GenericRestControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var model_type in EntityTypes.model_types)
            {
                var entity_type = model_type.Key;
                var entity_request_types = model_type.Value[0];
                Type[] typeArgs = { entity_type, model_type.Value[0], model_type.Value[1]};
                var controller_type = typeof(GenericRestController<,,>).MakeGenericType(typeArgs).GetTypeInfo();
                feature.Controllers.Add(controller_type);
            }
        }
    }
}
