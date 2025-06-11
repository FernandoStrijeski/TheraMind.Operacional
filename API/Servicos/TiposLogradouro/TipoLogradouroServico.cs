using API.Core.Exceptions;
using API.modelos;
using API.Servicos;
using API.Servicos.TiposLogradouros;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Operacional.Servicos.TiposLogradouros
{
    public class TipoLogradouroServico : ServicoBase, ITipoLogradouroServico
    {
        private IConfiguration _configuration;
        private ITipoLogradouroRepo _tipoLogradouroRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public TipoLogradouroServico(
            IConfiguration configuration,
            ITipoLogradouroRepo tiposLogradouroRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _tipoLogradouroRepo = tiposLogradouroRepo;
        }

        public async Task<TipoLogradouro>? BuscarPorID(string tipoLogradouroID) => await _tipoLogradouroRepo.BuscarPorID(tipoLogradouroID);

        public async Task<List<TipoLogradouro>> BuscarTodos()
        {
            return await _tipoLogradouroRepo.BuscarFiltros();
        }

        public async Task<List<TipoLogradouro>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _tipoLogradouroRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<TipoLogradouro> Adicionar(TipoLogradouro tipoLogradouro)
        {
            await _tipoLogradouroRepo.Adicionar(tipoLogradouro);
            await Comitar();
            return tipoLogradouro;
        }

        public async Task<TipoLogradouro> Atualizar(TipoLogradouro tipoLogradouro)
        {
            await _tipoLogradouroRepo.Atualizar(tipoLogradouro);
            await Comitar();
            return tipoLogradouro;
        }

        public async Task Deletar(string tipoLogradouroID)
        {
            var tipoLogradouro = _tipoLogradouroRepo.BuscarPorID(tipoLogradouroID).Result;

            if (tipoLogradouro == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Tipo de logradouro não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _tipoLogradouroRepo.Deletar(tipoLogradouroID);
            await Comitar();

            return;
        }
    }
}
