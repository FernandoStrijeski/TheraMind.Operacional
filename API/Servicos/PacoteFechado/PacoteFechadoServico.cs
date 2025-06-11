using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.PacotesFechados;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.PacotesFechados
{
    public class PacoteFechadoServico : ServicoBase, IPacoteFechadoServico
    {
        private IConfiguration _configuration;
        private IPacoteFechadoRepo _pacoteFechadoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PacoteFechadoServico(
            IConfiguration configuration,
            IPacoteFechadoRepo pacoteFechadoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _pacoteFechadoRepo = pacoteFechadoRepo;
        }

        public async Task<PacoteFechado>? BuscarPorID(int pacoteFechadoID) => await _pacoteFechadoRepo.BuscarPorID(pacoteFechadoID);

        public async Task<List<PacoteFechado>> BuscarTodos()
        {
            return await _pacoteFechadoRepo.BuscarFiltros();
        }

        public async Task<PacoteFechado> Adicionar(PacoteFechado pacoteFechado)
        {
            await _pacoteFechadoRepo.Adicionar(pacoteFechado);
            await Comitar();
            return pacoteFechado;
        }

        public async Task<PacoteFechado> Atualizar(PacoteFechado pacoteFechado)
        {
            await _pacoteFechadoRepo.Atualizar(pacoteFechado);
            await Comitar();
            return pacoteFechado;
        }

        public async Task Deletar(int pacoteFechadoID)
        {
            var pacoteFechado = _pacoteFechadoRepo.BuscarPorID(pacoteFechadoID).Result;

            if (pacoteFechado == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Pacote fechado não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _pacoteFechadoRepo.Deletar(pacoteFechadoID);
            await Comitar();

            return;
        }
    }
}
