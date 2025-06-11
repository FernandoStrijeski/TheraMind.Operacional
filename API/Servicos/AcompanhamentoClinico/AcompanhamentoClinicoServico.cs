using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.AcompanhamentosClinicos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AcompanhamentosClinicos
{
    public class AcompanhamentoClinicoServico : ServicoBase, IAcompanhamentoClinicoServico
    {
        private IConfiguration _configuration;
        private IAcompanhamentoClinicoRepo _acompanhamentoClinicoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AcompanhamentoClinicoServico(
            IConfiguration configuration,
            IAcompanhamentoClinicoRepo acompanhamentoClinicoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _acompanhamentoClinicoRepo = acompanhamentoClinicoRepo;
        }

        public async Task<AcompanhamentoClinico>? BuscarPorID(Guid acompanhamentoClinicoID) => await _acompanhamentoClinicoRepo.BuscarPorID(acompanhamentoClinicoID);

        public async Task<List<AcompanhamentoClinico>> BuscarTodos() => await _acompanhamentoClinicoRepo.BuscarFiltros();       

        public async Task<List<AcompanhamentoClinico>> BuscarTodosPorProfissionalCliente(Guid profissionalID, Guid clienteID) {
            return await _acompanhamentoClinicoRepo.BuscarFiltros(x => x.ProfissionalId == profissionalID && x.ClienteId == clienteID);
        }

        public async Task<AcompanhamentoClinico> Adicionar(AcompanhamentoClinico acompanhamentoClinico)
        {
            await _acompanhamentoClinicoRepo.Adicionar(acompanhamentoClinico);
            await Comitar();
            return acompanhamentoClinico;
        }

        public async Task<AcompanhamentoClinico> Atualizar(AcompanhamentoClinico acompanhamentoClinico)
        {
            await _acompanhamentoClinicoRepo.Atualizar(acompanhamentoClinico);
            await Comitar();
            return acompanhamentoClinico;
        }

        public async Task Deletar(Guid acompanhamentoClinicoID)
        {
            var acompanhamentoClinico = _acompanhamentoClinicoRepo.BuscarPorID(acompanhamentoClinicoID).Result;

            if (acompanhamentoClinico == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Acompanhamento não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _acompanhamentoClinicoRepo.Deletar(acompanhamentoClinicoID);
            await Comitar();

            return;
        }
    }
}
