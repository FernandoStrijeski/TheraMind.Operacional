using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.Paises
{
    public class PaisServico : ServicoBase, IPaisServico
    {
        private IConfiguration _configuration;
        private IPaisRepo _paisRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PaisServico(
            IConfiguration configuration,
            IPaisRepo paisRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _paisRepo = paisRepo;
        }

        public async Task<Pais>? BuscarPorID(int paisID) => await _paisRepo.BuscarPorID(paisID);

        public async Task<List<Pais>> BuscarTodos()
        {
            return await _paisRepo.BuscarFiltros();
        }

        public async Task<List<Pais>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _paisRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
