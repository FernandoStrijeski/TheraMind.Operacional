using System.Reflection;
using API.Core.Middleware;
using Infra.Servicos.MultiTenant.models;
using Microsoft.OpenApi.Models;
using API.Core.Extensoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using API.Core.Swagger;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TenantConfig>(Configuration.GetSection(nameof(TenantConfig)));

            services
                .AddResponseCompression()
                .AddHttpContextAccessor()
                .AdicionaInjecaoDeDependencia(Configuration)
                .AddControllers()
                .AddNewtonsoftJson(options =>
                    {
                        // Ignorar ciclos de referência no Newtonsoft.Json
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // Para evitar o loop
                    })
                    .AddJsonOptions(options =>
                    {
                        // Usar JsonStringEnumConverter para conversão de enums como strings
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    })
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AdicionarValidadores(Configuration);
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddAutoMapper(typeof(Startup));

            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])
                        )
                    };
                });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // Configure o formato da versão na rota
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddMvc()
                .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.ConfigureOptions<ConfigureSwaggerGenOptions>();

            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = @"O Token Bearer deve ser gerado na API de autenticação TheraMind.Autenticador - https://site.com/api/Autenticacao/swagger/index.html",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    }
                );

                options.MapType<DateOnly>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "date"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IServiceProvider serviceProvider
        )
        {
            var versionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseMiddleware(typeof(InterceptarErroMiddleware));
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            $"Web API - {description.GroupName.ToUpper()}");
                    }
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"./{description.GroupName}/swagger.json",
                        $"Web APi - {description.GroupName.ToUpper()}");
                }

                //c.SwaggerEndpoint("./v1/swagger.json", "API v1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();
        }
    }
}
