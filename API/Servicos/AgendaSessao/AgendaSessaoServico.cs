using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AgendasSessoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AgendasSessoes
{
    public class AgendaSessaoServico : ServicoBase, IAgendaSessaoServico
    {
        private IConfiguration _configuration;
        private IAgendaSessaoRepo _agendaSessaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaSessaoServico(
            IConfiguration configuration,
            IAgendaSessaoRepo agendaSessaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaSessaoRepo = agendaSessaoRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<AgendaSessao>? BuscarPorID(Guid agendaSessaoID) => await _agendaSessaoRepo.BuscarPorID(agendaSessaoID);

        public async Task<List<AgendaSessao>> BuscarTodos()
        {
            return await _agendaSessaoRepo.BuscarFiltros();
        }

        public async Task<List<AgendaSessao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _agendaSessaoRepo.BuscarFiltros(x => x.Cliente.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AgendaSessao> Adicionar(AgendaSessao agendaSessao)
        {
            await _agendaSessaoRepo.Adicionar(agendaSessao);
            await Comitar();
            return agendaSessao;
        }

        public async Task<AgendaSessao> Atualizar(AgendaSessao agendaSessao)
        {
            await _agendaSessaoRepo.Atualizar(agendaSessao);
            await Comitar();
            return agendaSessao;
        }

        public async Task Deletar(Guid agendaSessaoID)
        {
            var agendaSessao = _agendaSessaoRepo.BuscarPorID(agendaSessaoID).Result;

            if (agendaSessao == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Sessão não encontrada na agenda, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _agendaSessaoRepo.Deletar(agendaSessaoID);
            await Comitar();

            return;
        }
    }
}
