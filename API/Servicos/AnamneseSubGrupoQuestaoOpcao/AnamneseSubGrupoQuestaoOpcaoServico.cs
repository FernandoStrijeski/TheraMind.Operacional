using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseSubGrupoQuestaoOpcoes;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AnamneseSubGrupoQuestaoOpcoes
{
    public class AnamneseSubGrupoQuestaoOpcaoServico : ServicoBase, IAnamneseSubGrupoQuestaoOpcaoServico
    {
        private IConfiguration _configuration;
        private IAnamneseSubGrupoQuestaoOpcaoRepo _anamneseSubGrupoQuestaoOpcaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseSubGrupoQuestaoOpcaoServico(
            IConfiguration configuration,
            IAnamneseSubGrupoQuestaoOpcaoRepo anamneseSubGrupoQuestaoOpcaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseSubGrupoQuestaoOpcaoRepo = anamneseSubGrupoQuestaoOpcaoRepo;
        }

        public async Task<AnamneseSubGrupoQuestaoOpcao>? BuscarPorID(int anamneseSubGrupoQuestaoOpcaoID) => await _anamneseSubGrupoQuestaoOpcaoRepo.BuscarPorID(anamneseSubGrupoQuestaoOpcaoID);

        public async Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarTodos()
        {
            return await _anamneseSubGrupoQuestaoOpcaoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseSubGrupoQuestaoOpcaoRepo.BuscarFiltros(x => x.Texto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AnamneseSubGrupoQuestaoOpcao> Adicionar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao)
        {
            await _anamneseSubGrupoQuestaoOpcaoRepo.Adicionar(anamneseSubGrupoQuestaoOpcao);
            await Comitar();
            return anamneseSubGrupoQuestaoOpcao;
        }

        public async Task<AnamneseSubGrupoQuestaoOpcao> Atualizar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao)
        {
            await _anamneseSubGrupoQuestaoOpcaoRepo.Atualizar(anamneseSubGrupoQuestaoOpcao);
            await Comitar();
            return anamneseSubGrupoQuestaoOpcao;
        }

        public async Task Deletar(int anamneseSubGrupoQuestaoOpcaoID)
        {
            var anamneseSubGrupoQuestaoOpcao = _anamneseSubGrupoQuestaoOpcaoRepo.BuscarPorID(anamneseSubGrupoQuestaoOpcaoID).Result;

            if (anamneseSubGrupoQuestaoOpcao == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Opção da questão da anamnese não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _anamneseSubGrupoQuestaoOpcaoRepo.Deletar(anamneseSubGrupoQuestaoOpcaoID);
            await Comitar();

            return;
        }
    }
}
