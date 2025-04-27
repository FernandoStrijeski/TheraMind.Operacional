using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.TiposEtnias
{
    public class TipoEtniaServico : ServicoBase, ITipoEtniaServico
    {
        private IConfiguration _configuration;
        private ITipoEtniaRepo _tipoEtniaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public TipoEtniaServico(
            IConfiguration configuration,
            ITipoEtniaRepo tipoEtniaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _tipoEtniaRepo = tipoEtniaRepo;
        }

        public async Task<TipoEtnia>? BuscarPorID(int tipoEtniaID) => await _tipoEtniaRepo.BuscarPorID(tipoEtniaID);

        public async Task<List<TipoEtnia>> BuscarTodos()
        {
            return await _tipoEtniaRepo.BuscarFiltros();
        }

        public async Task<List<TipoEtnia>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _tipoEtniaRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
