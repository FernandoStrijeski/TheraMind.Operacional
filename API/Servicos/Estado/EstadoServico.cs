using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Estados
{
    public class EstadoServico : ServicoBase, IEstadoServico
    {
        private IConfiguration _configuration;
        private IEstadoRepo _estadoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EstadoServico(
            IConfiguration configuration,
            IEstadoRepo estadoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _estadoRepo = estadoRepo;
        }

        public async Task<Estado>? BuscarPorID(string uf) => await _estadoRepo.BuscarPorID(uf);

        public async Task<List<Estado>> BuscarTodos()
        {
            return await _estadoRepo.BuscarFiltros();
        }

        public async Task<List<Estado>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _estadoRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Estado> Adicionar(Estado estado)
        {
            await _estadoRepo.Adicionar(estado);
            await Comitar();
            return estado;
        }

        public async Task<Estado> Atualizar(Estado estado)
        {
            await _estadoRepo.Atualizar(estado);
            await Comitar();
            return estado;
        }

        public async Task Deletar(string uf)
        {
            var estado = _estadoRepo.BuscarPorID(uf).Result;

            if (estado == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Estado não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _estadoRepo.Deletar(uf);
            await Comitar();

            return;
        }
    }
}
