using Microsoft.AspNetCore.Identity.Data;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Build;

public class AddHeadersFilter:IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-ApplicationId",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("Swagger")
            },
            Required = true
        });
    }

}