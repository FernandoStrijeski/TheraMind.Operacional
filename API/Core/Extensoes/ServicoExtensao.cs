using API.Core.Util;
using API.Servicos;
using API.Servicos.Empresas;
using Dominio.Core.Repositorios;
using Dominio.Repositorios;
using Dominio.Servico;
using Infra.Context;
using Infra.Core.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Infra.Servicos.Tabela;
using API.Servicos.TiposLogradouros;
using API.Servicos.TiposEtnias;
using API.Servicos.Filiais;

namespace API.Core.Extensoes
{
    public static class InjecaoDeDependenciaExtensao
    {
        public static IServiceCollection AdicionaInjecaoDeDependencia(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            // SERVICO SINGLETON
            services
                .AddHttpContextAccessor()
                .AddSingleton(config)
                .AddDbContext<ApplicationDbContext>()
                .AddMemoryCache()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<ApiVersionHelper>()               

                // SERVICO TRANSIENT
                //.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>()

                // REPOSITORIOS SCOPED
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IEmpresaRepo, EmpresaRepo>()
                .AddScoped<IFilialRepo, FilialRepo>()
                .AddScoped<ITipoLogradouroRepo, TipoLogradouroRepo>()
                .AddScoped<ITipoEtniaRepo, TipoEtniaRepo>()
                

                // SERVICOS SCOPED
                .AddScoped<IConnectionParamsServico, ConnectionParamsServico>()                
                .AddScoped<ITabelaService, TabelaServico>()
                .AddScoped<IEmpresaServico, EmpresaServico>()                                       
                .AddScoped<IFilialServico, FilialServico>()   
                .AddScoped<ITipoLogradouroServico, TiposLogradouroServico>()                
                .AddScoped<ITipoEtniaServico, TipoEtniaServico>()                

            // HTTP CONTEXT
            // .TryAddSingleton<FilialController>()
            ;
            return services;
        }
    }
}
