using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Core.Swagger
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
            => _apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateOpenApiInfo(description));
            }
        }

        private static OpenApiInfo CreateOpenApiInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "TheraMind Sistemas - Operacional API",                
                Version = description.ApiVersion.ToString(),
                Description = "API responsável pelas operações do sistema",
                Contact = new OpenApiContact
                {
                    Name = "TheraMind Sistemas ltda",    
                    Email = "suporte@theramindsistemas.com",
                    Url = new Uri("https://www.theramindsistemas.com.br")
                }            
            };

            if (description.IsDeprecated)
            {
                info.Description += " (deprecated)";
            }

            return info;
        }
    }
}
