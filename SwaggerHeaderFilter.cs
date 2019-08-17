using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace angular_jwt_auth_dotnetcore {
    public class SwaggerHeaderFilter : IOperationFilter
    {
    public void Apply(Operation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<IParameter>();
 
        operation.Parameters.Add(new NonBodyParameter
        {
            Name = "Authorization",
            In = "header",
            Type = "string",
            Required = true // set to false if this is optional
        });
    }
   }
}