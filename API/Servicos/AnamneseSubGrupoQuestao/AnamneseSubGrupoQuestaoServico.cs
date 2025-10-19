using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.AnamneseSubGrupoQuestoes
{
    public class AnamneseSubGrupoQuestaoServico : ServicoBase, IAnamneseSubGrupoQuestaoServico
    {
        private IConfiguration _configuration;
        private IAnamneseSubGrupoQuestaoRepo _anamneseSubGrupoQuestaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseSubGrupoQuestaoServico(
            IConfiguration configuration,
            IAnamneseSubGrupoQuestaoRepo anamneseSubGrupoQuestaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseSubGrupoQuestaoRepo = anamneseSubGrupoQuestaoRepo;
        }

        public async Task<AnamneseSubGrupoQuestao>? BuscarPorID(int anamneseSubGrupoQuestaoID) => await _anamneseSubGrupoQuestaoRepo.BuscarPorID(anamneseSubGrupoQuestaoID);

        public async Task<List<AnamneseSubGrupoQuestao>> BuscarTodos()
        {
            return await _anamneseSubGrupoQuestaoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseSubGrupoQuestao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseSubGrupoQuestaoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<AnamneseSubGrupoQuestao> Adicionar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao)
        {
            await _anamneseSubGrupoQuestaoRepo.Adicionar(anamneseSubGrupoQuestao);
            await Comitar();
            return anamneseSubGrupoQuestao;
        }

        public async Task<AnamneseSubGrupoQuestao> Atualizar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao)
        {
            await _anamneseSubGrupoQuestaoRepo.Atualizar(anamneseSubGrupoQuestao);
            await Comitar();
            return anamneseSubGrupoQuestao;
        }

        public async Task Deletar(int anamneseSubGrupoQuestaoID)
        {
            var anamneseSubGrupoQuestao = _anamneseSubGrupoQuestaoRepo.BuscarPorID(anamneseSubGrupoQuestaoID).Result;

            if (anamneseSubGrupoQuestao == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Questão do subgrupo da amamnese não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _anamneseSubGrupoQuestaoRepo.Deletar(anamneseSubGrupoQuestaoID);
            await Comitar();

            return;
        }
    }
}
