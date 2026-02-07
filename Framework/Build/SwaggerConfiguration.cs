using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Framework.Build;

public class AddHeadersFilter:IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<IOpenApiParameter>();

        /*operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-ApplicationId",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Default = new OpenApiString("Swagger")
            },
            Required = true
        });*/
    }

}