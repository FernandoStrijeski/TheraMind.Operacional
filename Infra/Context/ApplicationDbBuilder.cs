using Dominio.Entidades;
using Infra.Context.Builders;
using Infra.Core.Extensoes;
using Infra.Servicos.MultiTenant;
using Infra.Servicos.MultiTenant.models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString;
        private ConnectionParams _connectionParams;
        private readonly IConnectionParamsServico _connectionParamsServico;

        public ApplicationDbContext(
            DbContextOptions options,
            IConnectionParamsServico connectionParamsServico
        ) : base(options)
        {
            _connectionParamsServico = connectionParamsServico;
            _connectionString = _connectionParamsServico.PegarConnectionString();
            _connectionParams = _connectionParamsServico.ObterConnectionsParams();
        }

        public virtual DbSet<Cidade> Cidades { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Escolaridade> Escolaridades { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<EstadoCivil> EstadosCivis { get; set; } = null!;
        public virtual DbSet<Filial> Filiais { get; set; } = null!;
        public virtual DbSet<IdentidadeGenero> IdentidadesGeneros { get; set; } = null!;
        public virtual DbSet<Nacionalidade> Nacionalidades { get; set; } = null!;
        public virtual DbSet<OrientacaoSexual> OrientacoesSexuais { get; set; } = null!;
        public virtual DbSet<Pais> Paises { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TiposDocumentos { get; set; } = null!;
        public virtual DbSet<TipoEtnia> TiposEtnias { get; set; } = null!;
        public virtual DbSet<TipoLogradouro> TiposLogradouros { get; set; } = null!;        
        public virtual DbSet<AcompanhamentoClinico> AcompanhamentosClinicos { get; set; } = null!;
        public virtual DbSet<AgendaProfissional> AgendasProfissionais { get; set; } = null!;
        public virtual DbSet<AgendaSessao> AgendasSessoes { get; set; } = null!;
        public virtual DbSet<AgendaSessaoItem> AgendasSessoesItems { get; set; } = null!;
        public virtual DbSet<AnamneseGrupo> AnamnesesGrupos { get; set; } = null!;
        public virtual DbSet<AnamneseRespostaCliente> AnamnesesRespostaClientes { get; set; } = null!;
        public virtual DbSet<AnamneseSubGrupo> AnamnesesSubGrupos { get; set; } = null!;
        public virtual DbSet<AnamneseSubGrupoQuestao> AnamnesesSubGrupoQuestaos { get; set; } = null!;
        public virtual DbSet<AnamneseSubGrupoQuestaoOpcao> AnamnesesSubGrupoQuestaoOpcaos { get; set; } = null!;
        public virtual DbSet<Auditoria> Auditorias { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Convenio> Convenios { get; set; } = null!;
        public virtual DbSet<DocumentoModelo> DocumentosModelos { get; set; } = null!;
        public virtual DbSet<DocumentoModeloEmpresa> DocumentosModeloEmpresas { get; set; } = null!;
        public virtual DbSet<DocumentoModeloEmpresaOpcao> DocumentosModeloEmpresaOpcaos { get; set; } = null!;
        public virtual DbSet<DocumentoVariavel> DocumentosVariaveis { get; set; } = null!;
        public virtual DbSet<EmpresaAssinatura> EmpresasAssinaturas { get; set; } = null!;
        public virtual DbSet<EmpresaFatura> EmpresasFaturas { get; set; } = null!;
        public virtual DbSet<FormularioSessao> FormulariosSessaos { get; set; } = null!;
        public virtual DbSet<FormularioSessaoCampo> FormulariosSessaoCampos { get; set; } = null!;
        public virtual DbSet<GrauParentesco> GrausParentescos { get; set; } = null!;
        public virtual DbSet<ModeloAnamneseG> ModelosAnamneseGs { get; set; } = null!;
        public virtual DbSet<ModeloAnamneseSg> ModelosAnamneseSgs { get; set; } = null!;
        public virtual DbSet<ModeloAnamneseSgquestao> ModelosAnamneseSgquestaos { get; set; } = null!;
        public virtual DbSet<ModeloAnamneseSgquestaoO> ModelosAnamneseSgquestaoOs { get; set; } = null!;
        public virtual DbSet<PacoteFechado> PacotesFechados { get; set; } = null!;
        public virtual DbSet<Plano> Planos { get; set; } = null!;
        public virtual DbSet<Profissional> Profissionais { get; set; } = null!;
        public virtual DbSet<ProfissionalAcesso> ProfissionalAcessos { get; set; } = null!;
        public virtual DbSet<Sala> Salas { get; set; } = null!;
        public virtual DbSet<Servico> Servicos { get; set; } = null!;        
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            if (_connectionParams != null)
            {
                ConectarBancoFactory(optionsBuilder, _connectionParams);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder                
                .ApplyConfiguration(new AcompanhamentoClinicoModelBuilder())
                .ApplyConfiguration(new AgendaProfissionalModelBuilder())
                .ApplyConfiguration(new AgendaSessaoItemModelBuilder())
                .ApplyConfiguration(new AgendaSessaoModelBuilder())
                .ApplyConfiguration(new AnamneseGrupoModelBuilder())
                .ApplyConfiguration(new AnamneseRespostaClienteModelBuilder())
                .ApplyConfiguration(new AnamneseSubGrupoModelBuilder())
                .ApplyConfiguration(new AnamneseSubGrupoQuestaoModelBuilder())
                .ApplyConfiguration(new AnamneseSubGrupoQuestaoOpcaoModelBuilder())
                .ApplyConfiguration(new AuditoriaModelBuilder())
                .ApplyConfiguration(new CidadeModelBuilder())
                .ApplyConfiguration(new ClienteModelBuilder())
                .ApplyConfiguration(new ConvenioModelBuilder())
                .ApplyConfiguration(new DocumentoModeloEmpresaModelBuilder())
                .ApplyConfiguration(new DocumentoModeloEmpresaModelBuilder())
                .ApplyConfiguration(new DocumentoModeloModelBuilder())
                .ApplyConfiguration(new DocumentoVariavelModelBuilder())
                .ApplyConfiguration(new EmpresaAssinaturaModelBuilder())
                .ApplyConfiguration(new EmpresaFaturaModelBuilder())
                .ApplyConfiguration(new EmpresaModelBuilder())
                .ApplyConfiguration(new EscolaridadeModelBuilder())
                .ApplyConfiguration(new EstadoCivilModelBuilder())
                .ApplyConfiguration(new EstadoModelBuilder())
                .ApplyConfiguration(new FilialModelBuilder())
                .ApplyConfiguration(new FormularioSessaoCampoModelBuilder())
                .ApplyConfiguration(new FormularioSessaoModelBuilder())
                .ApplyConfiguration(new GrauParentescoModelBuilder())
                .ApplyConfiguration(new IdentidadeGeneroModelBuilder())
                .ApplyConfiguration(new ModeloAnamneseGModelBuilder())
                .ApplyConfiguration(new ModeloAnamneseSgModelBuilder())
                .ApplyConfiguration(new ModeloAnamneseSgquestaoModelBuilder())
                .ApplyConfiguration(new ModeloAnamneseSgquestaoOModelBuilder())
                .ApplyConfiguration(new NacionalidadeModelBuilder())
                .ApplyConfiguration(new OrientacaoSexualModelBuilder())
                .ApplyConfiguration(new PacoteFechadoModelBuilder())
                .ApplyConfiguration(new PaisModelBuilder())
                .ApplyConfiguration(new PlanoModelBuilder())
                .ApplyConfiguration(new ProfissionalAcessoModelBuilder())
                .ApplyConfiguration(new ProfissionalModelBuilder())
                .ApplyConfiguration(new SalaModelBuilder())
                .ApplyConfiguration(new ServicoModelBuilder())
                .ApplyConfiguration(new TipoDocumentoModelBuilder())
                .ApplyConfiguration(new TipoEtniaModelBuilder())
                .ApplyConfiguration(new TipoLogradouroModelBuilder())
                .ApplyConfiguration(new UsuarioModelBuilder())

                .UpperCaseModelBuilder();
        }

        private void ConectarBancoFactory(
            DbContextOptionsBuilder optionsBuilder,
            ConnectionParams connectionParams
        )
        {
            if (connectionParams.ConnectionString == null)
            {
                throw new Exception("ConnectionString não foi informado");
            }
            switch (connectionParams.XpoProvider.ToLower().Trim())
            {
                case "mssqlserver":
                {
                    optionsBuilder.UseSqlServer(connectionParams.ConnectionString);
                    break;
                }
                case "oracledb"
                or "oracle":
                {
                    optionsBuilder.UseOracle(connectionParams.ConnectionString, builder => builder.UseOracleSQLCompatibility("11"));
                    break;
                }
                case "postgresql"
                or "postgres":
                {
                    optionsBuilder.UseNpgsql(connectionParams.ConnectionString);
                    break;
                }

                case "mariadb"
                or "mysql":
                {
                    if (connectionParams.ServerVersion == null)
                    {
                        throw new Exception("Versão do banco não foi informada");
                    }

                    var serverVersion = GerarVersaoMySql(connectionParams.ServerVersion);
                    optionsBuilder.UseMySql(connectionParams.ConnectionString, serverVersion);
                    break;
                }
                default:

                    {
                        throw new Exception("Provider de banco não implementado");
                    }
                    ;
            }
        }

        private ServerVersion GerarVersaoMySql(string versao)
        {
            String[] separadores = { ".", "," };
            var v = versao.Split(separadores, 3, StringSplitOptions.RemoveEmptyEntries);
            return new MySqlServerVersion(
                new Version(int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]))
            );
        }
    }
}
