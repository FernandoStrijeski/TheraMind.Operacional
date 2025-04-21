using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.TiposEtnias
{
    public class TiposEtniaServico : ServicoBase, ITipoEtniaServico
    {
        private IConfiguration _configuration;
        private ITipoEtniaRepo _tiposEtniaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public TiposEtniaServico(
            IConfiguration configuration,
            ITipoEtniaRepo tiposEtniaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _tiposEtniaRepo = tiposEtniaRepo;
        }

        public async Task<TipoEtnia>? BuscarPorID(int tipoEtniaID) => await _tiposEtniaRepo.BuscarPorID(tipoEtniaID);

        public async Task<List<TipoEtnia>> BuscarTodos()
        {
            return await _tiposEtniaRepo.BuscarFiltros();
        }

        public async Task<List<TipoEtnia>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _tiposEtniaRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
