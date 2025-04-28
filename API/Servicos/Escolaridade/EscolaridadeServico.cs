using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.Escolaridades
{
    public class EscolaridadeServico : ServicoBase, IEscolaridadeServico
    {
        private IConfiguration _configuration;
        private IEscolaridadeRepo _escolaridadeRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EscolaridadeServico(
            IConfiguration configuration,
            IEscolaridadeRepo escolaridadeRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _escolaridadeRepo = escolaridadeRepo;
        }

        public async Task<Escolaridade>? BuscarPorID(int escolaridadeID) => await _escolaridadeRepo.BuscarPorID(escolaridadeID);

        public async Task<List<Escolaridade>> BuscarTodos()
        {
            return await _escolaridadeRepo.BuscarFiltros();
        }

        public async Task<List<Escolaridade>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _escolaridadeRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
