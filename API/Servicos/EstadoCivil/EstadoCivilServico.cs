using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.EstadosCivis
{
    public class EstadoCivilServico : ServicoBase, IEstadoCivilServico
    {
        private IConfiguration _configuration;
        private IEstadoCivilRepo _estadoCivilRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EstadoCivilServico(
            IConfiguration configuration,
            IEstadoCivilRepo estadoCivilRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _estadoCivilRepo = estadoCivilRepo;
        }

        public async Task<EstadoCivil>? BuscarPorID(string estadoCivilID) => await _estadoCivilRepo.BuscarPorID(estadoCivilID);

        public async Task<List<EstadoCivil>> BuscarTodos()
        {
            return await _estadoCivilRepo.BuscarFiltros();
        }

        public async Task<List<EstadoCivil>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _estadoCivilRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
