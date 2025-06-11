using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseRespostaClientes;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Org.BouncyCastle.Ocsp;
using System.Net;

namespace API.Servicos.AnamneseRespostaClientes
{
    public class AnamneseRespostaClienteServico : ServicoBase, IAnamneseRespostaClienteServico
    {
        private IConfiguration _configuration;
        private IAnamneseRespostaClienteRepo _anamneseRespostaClienteRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseRespostaClienteServico(
            IConfiguration configuration,
            IAnamneseRespostaClienteRepo anamneseRespostaClienteRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseRespostaClienteRepo = anamneseRespostaClienteRepo;
        }

        public async Task<AnamneseRespostaCliente>? BuscarPorID(int anamneseSubGrupoQuestaoID) => await _anamneseRespostaClienteRepo.BuscarPorID(anamneseSubGrupoQuestaoID);

        public async Task<List<AnamneseRespostaCliente>> BuscarTodos()
        {
            return await _anamneseRespostaClienteRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseRespostaCliente>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseRespostaClienteRepo.BuscarFiltros(x => x.Resposta.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AnamneseRespostaCliente> Adicionar(AnamneseRespostaCliente anamneseRespostaCliente)
        {
            await _anamneseRespostaClienteRepo.Adicionar(anamneseRespostaCliente);
            await Comitar();
            return anamneseRespostaCliente;
        }

        public async Task<AnamneseRespostaCliente> Atualizar(AnamneseRespostaCliente anamneseRespostaCliente)
        {
            await _anamneseRespostaClienteRepo.Atualizar(anamneseRespostaCliente);
            await Comitar();
            return anamneseRespostaCliente;
        }

        public async Task Deletar(int anamneseSubGrupoQuestaoID)
        {
            var anamneseRespostaCliente = _anamneseRespostaClienteRepo.BuscarPorID(anamneseSubGrupoQuestaoID).Result;

            if (anamneseRespostaCliente == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Resposta do cliente não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _anamneseRespostaClienteRepo.Deletar(anamneseSubGrupoQuestaoID);
            await Comitar();

            return;
        }
    }
}
