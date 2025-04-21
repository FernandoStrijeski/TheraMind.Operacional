using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.TiposLogradouros
{
    public class TiposLogradouroServico : ServicoBase, ITipoLogradouroServico
    {
        private IConfiguration _configuration;
        private ITipoLogradouroRepo _tiposLogradouroRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public TiposLogradouroServico(
            IConfiguration configuration,
            ITipoLogradouroRepo tiposLogradouroRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _tiposLogradouroRepo = tiposLogradouroRepo;
        }

        public async Task<TipoLogradouro>? BuscarPorID(string tipoLogradouroID) => await _tiposLogradouroRepo.BuscarPorID(tipoLogradouroID);

        public async Task<List<TipoLogradouro>> BuscarTodos()
        {
            return await _tiposLogradouroRepo.BuscarFiltros();
        }

        public async Task<List<TipoLogradouro>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _tiposLogradouroRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }
    }
}
