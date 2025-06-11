using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

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

        public async Task<TipoEtnia> Adicionar(TipoEtnia tipoEtnia)
        {
            await _tipoEtniaRepo.Adicionar(tipoEtnia);
            await Comitar();
            return tipoEtnia;
        }

        public async Task<TipoEtnia> Atualizar(TipoEtnia tipoEtnia)
        {
            await _tipoEtniaRepo.Atualizar(tipoEtnia);
            await Comitar();
            return tipoEtnia;
        }

        public async Task Deletar(int tipoEtniaID)
        {
            var tipoEtnia = _tipoEtniaRepo.BuscarPorID(tipoEtniaID).Result;

            if (tipoEtnia == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Tipo de etnia não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _tipoEtniaRepo.Deletar(tipoEtniaID);
            await Comitar();

            return;
        }
    }
}
