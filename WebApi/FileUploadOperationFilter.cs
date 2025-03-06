using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var formFileParam = context.ApiDescription.ParameterDescriptions
            .FirstOrDefault(p => p.ParameterDescriptor.ParameterType == typeof(IFormFile));

        if (formFileParam != null)
        {
            var fileParam = operation.Parameters.FirstOrDefault(p => p.Name == formFileParam.Name);
            if (fileParam != null)
            {
                fileParam.Schema.Type = "string";
                fileParam.Schema.Format = "binary"; // Указываем, что это файл
            }
        }
    }
}