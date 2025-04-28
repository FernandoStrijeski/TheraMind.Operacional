using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;

namespace API.Servicos.OrientacoesSexuais
{
    public class OrientacaoSexualServico : ServicoBase, IOrientacaoSexualServico
    {
        private IConfiguration _configuration;
        private IOrientacaoSexualRepo _orientacaoSexualRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public OrientacaoSexualServico(
            IConfiguration configuration,
            IOrientacaoSexualRepo orientacaoSexualRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _orientacaoSexualRepo = orientacaoSexualRepo;
        }

        public async Task<OrientacaoSexual>? BuscarPorID(int orientacaoSexualID) => await _orientacaoSexualRepo.BuscarPorID(orientacaoSexualID);

        public async Task<List<OrientacaoSexual>> BuscarTodos()
        {
            return await _orientacaoSexualRepo.BuscarFiltros();
        }

        public async Task<List<OrientacaoSexual>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _orientacaoSexualRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
