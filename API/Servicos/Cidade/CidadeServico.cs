using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.Cidades
{
    public class CidadeServico : ServicoBase, ICidadeServico
    {
        private IConfiguration _configuration;
        private ICidadeRepo _cidadeRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public CidadeServico(
            IConfiguration configuration,
            ICidadeRepo cidadeRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _cidadeRepo = cidadeRepo;
        }

        public async Task<Cidade>? BuscarPorID(int cidadeId) => await _cidadeRepo.BuscarPorID(cidadeId);

        public async Task<List<Cidade>> BuscarTodos()
        {
            return await _cidadeRepo.BuscarFiltros();
        }

        public async Task<List<Cidade>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _cidadeRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Cidade>? BuscarPorIBGE(int codigoIBGE) => await _cidadeRepo.BuscarPorIBGE(codigoIBGE);
    }
}
