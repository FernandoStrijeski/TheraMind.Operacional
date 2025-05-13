using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.GrauParentescos
{
    public class GrauParentescoServico : ServicoBase, IGrauParentescoServico
    {
        private IConfiguration _configuration;
        private IGrauParentescoRepo _grauParentescoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public GrauParentescoServico(
            IConfiguration configuration,
            IGrauParentescoRepo grauParentescoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _grauParentescoRepo = grauParentescoRepo;
        }

        public async Task<GrauParentesco>? BuscarPorID(int nacionalidadeID) => await _grauParentescoRepo.BuscarPorID(nacionalidadeID);

        public async Task<List<GrauParentesco>> BuscarTodos()
        {
            return await _grauParentescoRepo.BuscarFiltros();
        }

        public async Task<List<GrauParentesco>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _grauParentescoRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
