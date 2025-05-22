using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace API.Core.Utils
{
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeSpan))
            {
                // Altera o schema para tipo string
                schema.Type = "string";
                schema.Format = "time-span";
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString("08:30:00");
            }
        }
    }

}
