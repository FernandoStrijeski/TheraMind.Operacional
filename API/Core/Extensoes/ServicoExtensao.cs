using API.Core.Util;
using API.Operacional.Servicos.TiposLogradouros;
using API.Servicos.AgendaSessaoItens;
using API.Servicos.AgendasProfissionais;
using API.Servicos.AgendasSessaoItens;
using API.Servicos.AgendasSessoes;
using API.Servicos.Auditorias;
using API.Servicos.Boletos;
using API.Servicos.Cidades;
using API.Servicos.Clientes;
using API.Servicos.Convenios;
using API.Servicos.DocumentosModelos;
using API.Servicos.DocumentosModelosEmpresas;
using API.Servicos.DocumentosModelosEmpresasOpcoes;
using API.Servicos.DocumentosVariaveis;
using API.Servicos.Empresas;
using API.Servicos.EmpresasAssinaturas;
using API.Servicos.Escolaridades;
using API.Servicos.Estados;
using API.Servicos.EstadosCivis;
using API.Servicos.Filiais;
using API.Servicos.FormularioSessaoCampos;
using API.Servicos.FormulariosSessoes;
using API.Servicos.GeradorCNAB240Sicredi;
using API.Servicos.GrauParentescos;
using API.Servicos.IdentidadesGeneros;
using API.Servicos.Nacionalidades;
using API.Servicos.OrientacoesSexuais;
using API.Servicos.PacotesFechados;
using API.Servicos.Paises;
using API.Servicos.Planos;
using API.Servicos.Profissionais;
using API.Servicos.ProfissionaisAcessos;
using API.Servicos.Salas;
using API.Servicos.Servicos;
using API.Servicos.TiposDocumentos;
using API.Servicos.TiposEtnias;
using API.Servicos.TiposLogradouros;
using API.Servicos.Usuarios;
using Dominio.AgendasProfissionais;
using Dominio.AgendasSessaoItens;
using Dominio.AgendasSessoes;
using Dominio.Clientes;
using Dominio.Convenios;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelos;
using Dominio.DocumentosModelosEmpresas;
using Dominio.DocumentosModelosEmpresasOpcoes;
using Dominio.DocumentosVariaveis;
using Dominio.EmpresasAssinaturas;
using Dominio.Entidades;
using Dominio.FormulariosSessaoCampos;
using Dominio.FormulariosSessoes;
using Dominio.PacotesFechados;
using Dominio.Profissionais;
using Dominio.ProfissionaisAcessos;
using Dominio.Repositorios;
using Dominio.Service;
using Infra.AgendasProfissionais;
using Infra.AgendasSessaoItens;
using Infra.AgendasSessoes;
using Infra.Clientes;
using Infra.Context;
using Infra.Convenios;
using Infra.Core.Repositorios;
using Infra.DocumentosModelos;
using Infra.DocumentosModelosEmpresas;
using Infra.DocumentosModelosEmpresasOpcoes;
using Infra.DocumentosVariaveis;
using Infra.EmpresasAssinaturas;
using Infra.FormularioSessaoCampos;
using Infra.FormulariosSessoes;
using Infra.PacotesFechados;
using Infra.Profissionais;
using Infra.ProfissionaisAcessos;
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
                .AddScoped<IProfissionalRepo, ProfissionalRepo>()
                .AddScoped<IProfissionalAcessoRepo, ProfissionalAcessoRepo>()
                .AddScoped<IConvenioRepo, ConvenioRepo>()
                .AddScoped<IPacoteFechadoRepo, PacoteFechadoRepo>()
                .AddScoped<IEmpresaAssinaturaRepo, EmpresaAssinaturaRepo>()
                .AddScoped<IClienteRepo, ClienteRepo>()
                .AddScoped<IDocumentoVariavelRepo, DocumentoVariavelRepo>()
                .AddScoped<IAgendaProfissionalRepo, AgendaProfissionalRepo>()
                .AddScoped<IDocumentoModeloRepo, DocumentoModeloRepo>()
                .AddScoped<IDocumentoModeloEmpresaRepo, DocumentoModeloEmpresaRepo>()
                .AddScoped<IDocumentoModeloEmpresaOpcaoRepo, DocumentoModeloEmpresaOpcaoRepo>()
                .AddScoped<IAgendaSessaoRepo, AgendaSessaoRepo>()
                .AddScoped<IAgendaSessaoItemRepo, AgendaSessaoItemRepo>()
                .AddScoped<IFormularioSessaoRepo, FormularioSessaoRepo>()
                .AddScoped<IFormularioSessaoCampoRepo, FormularioSessaoCampoRepo>()                

                

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
                .AddScoped<IBoletoServico, BoletoServico>()                
                .AddScoped<IGeradorCNAB240SicrediServico, GeradorCNAB240SicrediServico>()                
                .AddScoped<IProfissionalServico, ProfissionalServico>()                
                .AddScoped<IProfissionalAcessoServico, ProfissionalAcessoServico>()                
                .AddScoped<IConvenioServico, ConvenioServico>()                
                .AddScoped<IPacoteFechadoServico, PacoteFechadoServico>()                
                .AddScoped<IEmpresaAssinaturaServico, EmpresaAssinaturaServico>()                
                .AddScoped<IClienteServico, ClienteServico>()                
                .AddScoped<IDocumentoVariavelServico, DocumentoVariavelServico>()                
                .AddScoped<IAgendaProfissionalServico, AgendaProfissionalServico>()                
                .AddScoped<IDocumentoModeloServico, DocumentoModeloServico>()                
                .AddScoped<IDocumentoModeloEmpresaServico, DocumentoModeloEmpresaServico>()                
                .AddScoped<IDocumentoModeloEmpresaOpcaoServico, DocumentoModeloEmpresaOpcaoServico>()                
                .AddScoped<IAgendaSessaoServico, AgendaSessaoServico>()                
                .AddScoped<IAgendaSessaoItemServico, AgendaSessaoItemServico>()          
                .AddScoped<IFormularioSessaoServico, FormularioSessaoServico>()                
                .AddScoped<IFormularioSessaoCampoServico, FormularioSessaoCampoServico>()                

            // HTTP CONTEXT
            // .TryAddSingleton<FilialController>()
            ;
            return services;
        }
    }
}
