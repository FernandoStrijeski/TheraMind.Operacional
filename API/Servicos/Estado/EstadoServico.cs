using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.Estados
{
    public class EstadoServico : ServicoBase, IEstadoServico
    {
        private IConfiguration _configuration;
        private IEstadoRepo _estadoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EstadoServico(
            IConfiguration configuration,
            IEstadoRepo estadoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _estadoRepo = estadoRepo;
        }

        public async Task<Estado>? BuscarPorID(string uf) => await _estadoRepo.BuscarPorID(uf);

        public async Task<List<Estado>> BuscarTodos()
        {
            return await _estadoRepo.BuscarFiltros();
        }

        public async Task<List<Estado>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _estadoRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
