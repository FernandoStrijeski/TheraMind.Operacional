using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AgendasProfissionais;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.AgendasProfissionais
{
    public class AgendaProfissionalServico : ServicoBase, IAgendaProfissionalServico
    {
        private IConfiguration _configuration;
        private IAgendaProfissionalRepo _agendaProfissionalRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaProfissionalServico(
            IConfiguration configuration,
            IAgendaProfissionalRepo agendaProfissionalRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaProfissionalRepo = agendaProfissionalRepo;
        }

        public async Task<AgendaProfissional>? BuscarPorID(int agendaProfissionalID) => await _agendaProfissionalRepo.BuscarPorID(agendaProfissionalID);

        public async Task<List<AgendaProfissional>> BuscarTodos()
        {
            return await _agendaProfissionalRepo.BuscarFiltros();
        }

        public async Task<AgendaProfissional> Adicionar(AgendaProfissional agendaProfissional)
        {
            await _agendaProfissionalRepo.Adicionar(agendaProfissional);
            await Comitar();
            return agendaProfissional;
        }

        public async Task<AgendaProfissional> Atualizar(AgendaProfissional agendaProfissional)
        {
            await _agendaProfissionalRepo.Atualizar(agendaProfissional);
            await Comitar();
            return agendaProfissional;
        }

        public async Task Deletar(int agendaProfissionalID)
        {
            var agendaProfissional = _agendaProfissionalRepo.BuscarPorID(agendaProfissionalID).Result;

            if (agendaProfissional == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Agenda profissional não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _agendaProfissionalRepo.Deletar(agendaProfissionalID);
            await Comitar();

            return;
        }
    }
}
