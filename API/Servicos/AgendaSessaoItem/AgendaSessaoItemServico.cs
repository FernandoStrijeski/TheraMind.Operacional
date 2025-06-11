using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.AgendasSessaoItens;
using Dominio.AgendasSessaoItens;
using Dominio.AgendasSessoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AgendaSessaoItens
{
    public class AgendaSessaoItemServico : ServicoBase, IAgendaSessaoItemServico
    {
        private IConfiguration _configuration;
        private IAgendaSessaoItemRepo _agendaSessaoItemRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaSessaoItemServico(
            IConfiguration configuration,
            IAgendaSessaoItemRepo agendaSessaoItemRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaSessaoItemRepo = agendaSessaoItemRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<AgendaSessaoItem>? BuscarPorID(int agendaSessaoItemID) => await _agendaSessaoItemRepo.BuscarPorID(agendaSessaoItemID);

        public async Task<List<AgendaSessaoItem>> BuscarTodos()
        {
            return await _agendaSessaoItemRepo.BuscarFiltros();
        }

        public async Task<List<AgendaSessaoItem>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _agendaSessaoItemRepo.BuscarFiltros(x => x.Cliente.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AgendaSessaoItem> Adicionar(AgendaSessaoItem agendaSessaoItem)
        {
            await _agendaSessaoItemRepo.Adicionar(agendaSessaoItem);
            await Comitar();
            return agendaSessaoItem;
        }

        public async Task<AgendaSessaoItem> Atualizar(AgendaSessaoItem agendaSessaoItem)
        {
            await _agendaSessaoItemRepo.Atualizar(agendaSessaoItem);
            await Comitar();
            return agendaSessaoItem;
        }

        public async Task Deletar(int agendaSessaoItemID)
        {
            var escolaridade = _agendaSessaoItemRepo.BuscarPorID(agendaSessaoItemID).Result;

            if (escolaridade == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Item da sessão da agenda não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _agendaSessaoItemRepo.Deletar(agendaSessaoItemID);
            await Comitar();

            return;
        }
    }
}
