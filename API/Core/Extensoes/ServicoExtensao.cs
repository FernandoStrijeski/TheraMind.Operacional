using API.Core.Util;
using API.Operacional.Servicos.TiposLogradouros;
using API.Servicos.AcompanhamentosClinicos;
using API.Servicos.AgendaSessaoItens;
using API.Servicos.AgendasProfissionais;
using API.Servicos.AgendasSessaoItens;
using API.Servicos.AgendasSessoes;
using API.Servicos.AnamneseGrupos;
using API.Servicos.AnamneseRespostaClientes;
using API.Servicos.AnamneseSubGrupoQuestaoOpcoes;
using API.Servicos.AnamneseSubGrupoQuestoes;
using API.Servicos.AnamneseSubGrupos;
using API.Servicos.Auditorias;
using API.Servicos.Boletos;
using API.Servicos.Cidades;
using API.Servicos.Clientes;
using API.Servicos.Convenios;
using API.Servicos.DocumentosModelos;
using API.Servicos.DocumentosModelosEmpresas;
using API.Servicos.DocumentosModelosEmpresasOpcoes;
using API.Servicos.DocumentosVariaveis;
using API.Servicos.EmpresaFaturas;
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
using API.Servicos.ModelosAnamneseG;
using API.Servicos.ModelosAnamneseSG;
using API.Servicos.ModelosAnamneseSGQuestaoOpcoes;
using API.Servicos.ModelosAnamneseSGQuestoes;
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
using Dominio.AcompanhamentosClinicos;
using Dominio.AgendasProfissionais;
using Dominio.AgendasSessaoItens;
using Dominio.AgendasSessoes;
using Dominio.AnamneseGrupos;
using Dominio.AnamneseRespostaClientes;
using Dominio.AnamneseSubGrupoQuestaoOpcoes;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.AnamneseSubGrupos;
using Dominio.Clientes;
using Dominio.Convenios;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelos;
using Dominio.DocumentosModelosEmpresas;
using Dominio.DocumentosModelosEmpresasOpcoes;
using Dominio.DocumentosVariaveis;
using Dominio.EmpresaFaturas;
using Dominio.EmpresasAssinaturas;
using Dominio.Entidades;
using Dominio.FormulariosSessaoCampos;
using Dominio.FormulariosSessoes;
using Dominio.ModelosAnamneseG;
using Dominio.ModelosAnamneseSG;
using Dominio.ModelosAnamneseSGQuestaoOpcoes;
using Dominio.ModelosAnamneseSGQuestoes;
using Dominio.PacotesFechados;
using Dominio.Profissionais;
using Dominio.ProfissionaisAcessos;
using Dominio.Repositorios;
using Dominio.Service;
using Infra.AcompanhamentosClinicos;
using Infra.AgendasProfissionais;
using Infra.AgendasSessaoItens;
using Infra.AgendasSessoes;
using Infra.AnamneseGrupos;
using Infra.AnamneseRespostaClientes;
using Infra.AnamneseSubGrupoQuestaoOpcoes;
using Infra.AnamneseSubGrupoQuestoes;
using Infra.AnamneseSubGrupos;
using Infra.Clientes;
using Infra.Context;
using Infra.Convenios;
using Infra.Core.Repositorios;
using Infra.DocumentosModelos;
using Infra.DocumentosModelosEmpresas;
using Infra.DocumentosModelosEmpresasOpcoes;
using Infra.DocumentosVariaveis;
using Infra.EmpresaFaturas;
using Infra.EmpresasAssinaturas;
using Infra.FormularioSessaoCampos;
using Infra.FormulariosSessoes;
using Infra.ModelosAnamneseG;
using Infra.ModelosAnamneseSG;
using Infra.ModelosAnamneseSGQuestaoOpcoes;
using Infra.ModelosAnamneseSGQuestoes;
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
                .AddScoped<IAcompanhamentoClinicoRepo, AcompanhamentoClinicoRepo>()                
                .AddScoped<IEmpresaFaturaRepo, EmpresaFaturaRepo>()                
                .AddScoped<IModeloAnamneseGRepo, ModeloAnamneseGRepo>()                
                .AddScoped<IModeloAnamneseSgRepo, ModeloAnamneseSgRepo>()                
                .AddScoped<IModeloAnamneseSgQuestaoRepo, ModeloAnamneseSgQuestaoRepo>()                
                .AddScoped<IModeloAnamneseSgQuestaoORepo, ModeloAnamneseSgQuestaoORepo>()                
                .AddScoped<IAnamneseGrupoRepo, AnamneseGrupoRepo>()                
                .AddScoped<IAnamneseSubGrupoRepo, AnamneseSubGrupoRepo>()                
                .AddScoped<IAnamneseSubGrupoQuestaoRepo, AnamneseSubGrupoQuestaoRepo>()                
                .AddScoped<IAnamneseSubGrupoQuestaoOpcaoRepo, AnamneseSubGrupoQuestaoOpcaoRepo>()                
                .AddScoped<IAnamneseRespostaClienteRepo, AnamneseRespostaClienteRepo>()                

                

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
                .AddScoped<IAcompanhamentoClinicoServico, AcompanhamentoClinicoServico>()                
                .AddScoped<IEmpresaFaturaServico, EmpresaFaturaServico>()                
                .AddScoped<IModeloAnamneseGServico, ModeloAnamneseGServico>()                
                .AddScoped<IModeloAnamneseSgServico, ModeloAnamneseSgServico>()                
                .AddScoped<IModeloAnamneseSgQuestaoServico, ModeloAnamneseSgQuestaoServico>()                
                .AddScoped<IModeloAnamneseSgQuestaoOServico, ModeloAnamneseSgQuestaoOServico>()                
                .AddScoped<IAnamneseGrupoServico, AnamneseGrupoServico>()                
                .AddScoped<IAnamneseSubGrupoServico, AnamneseSubGrupoServico>()                
                .AddScoped<IAnamneseSubGrupoQuestaoServico, AnamneseSubGrupoQuestaoServico>()                
                .AddScoped<IAnamneseSubGrupoQuestaoOpcaoServico, AnamneseSubGrupoQuestaoOpcaoServico>()                
                .AddScoped<IAnamneseRespostaClienteServico, AnamneseRespostaClienteServico>()                

            // HTTP CONTEXT
            // .TryAddSingleton<FilialController>()
            ;
            return services;
        }
    }
}
