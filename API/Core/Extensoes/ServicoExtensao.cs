using API.Core.Util;
using API.Operacional.Servicos.TiposLogradouros;
using API.Servicos.Auditorias;
using API.Servicos.Cidades;
using API.Servicos.Empresas;
using API.Servicos.Escolaridades;
using API.Servicos.Estados;
using API.Servicos.EstadosCivis;
using API.Servicos.Filiais;
using API.Servicos.GrauParentescos;
using API.Servicos.IdentidadesGeneros;
using API.Servicos.Nacionalidades;
using API.Servicos.OrientacoesSexuais;
using API.Servicos.Paises;
using API.Servicos.Planos;
using API.Servicos.Salas;
using API.Servicos.Servicos;
using API.Servicos.TiposDocumentos;
using API.Servicos.TiposEtnias;
using API.Servicos.TiposLogradouros;
using API.Servicos.Usuarios;
using Dominio.Core.Repositorios;
using Dominio.Repositorios;
using Dominio.Service;
using Infra.Context;
using Infra.Core.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Infra.Servicos.Tabela;

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
                .AddScoped<ITipoDocumentoRepo, TipoDocumentoRepo>()
                .AddScoped<IOrientacaoSexualRepo, OrientacaoSexualRepo>()
                .AddScoped<IIdentidadeGeneroRepo, IdentidadeGeneroRepo>()
                .AddScoped<IEstadoCivilRepo, EstadoCivilRepo>()
                .AddScoped<IEscolaridadeRepo, EscolaridadeRepo>()
                .AddScoped<IPaisRepo, PaisRepo>()
                .AddScoped<IEstadoRepo, EstadoRepo>()
                .AddScoped<ICidadeRepo, CidadeRepo>()
                .AddScoped<INacionalidadeRepo, NacionalidadeRepo>()
                .AddScoped<IGrauParentescoRepo, GrauParentescoRepo>()
                .AddScoped<IAuditoriaRepo, AuditoriaRepo>()
                .AddScoped<IPlanoRepo, PlanoRepo>()
                .AddScoped<IServicoRepo, ServicoRepo>()
                .AddScoped<ISalaRepo, SalaRepo>()
                .AddScoped<IUsuarioRepo, UsuarioRepo>()

                

                // SERVICOS SCOPED
                .AddScoped<IConnectionParamsServico, ConnectionParamsServico>()                
                .AddScoped<ITabelaService, TabelaServico>()
                .AddScoped<IEmpresaServico, EmpresaServico>()                                       
                .AddScoped<IFilialServico, FilialServico>()   
                .AddScoped<ITipoLogradouroServico, TipoLogradouroServico>()                
                .AddScoped<ITipoEtniaServico, TipoEtniaServico>()                
                .AddScoped<ITipoDocumentoServico, TipoDocumentoServico>()                
                .AddScoped<IOrientacaoSexualServico, OrientacaoSexualServico>()                
                .AddScoped<IIdentidadeGeneroServico, IdentidadeGeneroServico>()                
                .AddScoped<IEstadoCivilServico, EstadoCivilServico>()                
                .AddScoped<IEscolaridadeServico, EscolaridadeServico>()                
                .AddScoped<IPaisServico, PaisServico>()                
                .AddScoped<IEstadoServico, EstadoServico>()                
                .AddScoped<ICidadeServico, CidadeServico>()                
                .AddScoped<INacionalidadeServico, NacionalidadeServico>()                
                .AddScoped<IGrauParentescoServico, GrauParentescoServico>()                
                .AddScoped<IAuditoriaServico, AuditoriaServico>()                
                .AddScoped<IPlanoServico, PlanoServico>()                
                .AddScoped<IServicoServico, ServicoServico>()                
                .AddScoped<ISalaServico, SalaServico>()                
                .AddScoped<IUsuarioServico, UsuarioServico>()                

            // HTTP CONTEXT
            // .TryAddSingleton<FilialController>()
            ;
            return services;
        }
    }
}
