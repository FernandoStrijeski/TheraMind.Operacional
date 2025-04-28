using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.IdentidadesGeneros
{
    public class IdentidadeGeneroServico : ServicoBase, IIdentidadeGeneroServico
    {
        private IConfiguration _configuration;
        private IIdentidadeGeneroRepo _identidadeGeneroRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public IdentidadeGeneroServico(
            IConfiguration configuration,
            IIdentidadeGeneroRepo identidadeGeneroRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _identidadeGeneroRepo = identidadeGeneroRepo;
        }

        public async Task<IdentidadeGenero>? BuscarPorID(int identidadeGeneroID) => await _identidadeGeneroRepo.BuscarPorID(identidadeGeneroID);

        public async Task<List<IdentidadeGenero>> BuscarTodos()
        {
            return await _identidadeGeneroRepo.BuscarFiltros();
        }

        public async Task<List<IdentidadeGenero>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _identidadeGeneroRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
