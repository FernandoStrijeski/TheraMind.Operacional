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

        public async Task Salvar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao)
        {
            await _anamneseSubGrupoQuestaoOpcaoRepo.Adicionar(anamneseSubGrupoQuestaoOpcao);
            await Comitar();
        }

        private async Task Atualizar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao)
        {
            await _anamneseSubGrupoQuestaoOpcaoRepo.Atualizar(anamneseSubGrupoQuestaoOpcao);
            await Comitar();
        }

        public async Task<(bool criado, int anamneseSubGrupoQuestaoOpcaoId)> CriarOuAtualizar(CriarAnamneseSubGrupoQuestaoOpcaoInputModel anamneseSubGrupoQuestaoOpcao, bool atualizaSeExistir)
        {
            var cAnamneseSubGrupoQuestaoOpcao = (await _anamneseSubGrupoQuestaoOpcaoRepo.Buscar(
                x => x.AnamneseSubGrupoQuestaoOpcaoId == anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoOpcaoId
            )).FirstOrDefault();

            if (cAnamneseSubGrupoQuestaoOpcao == null)
            {
                cAnamneseSubGrupoQuestaoOpcao = AnamneseSubGrupoQuestaoOpcao.CriarParaImportacao(
                    empresaID: anamneseSubGrupoQuestaoOpcao.EmpresaId,
                    filialID: anamneseSubGrupoQuestaoOpcao.FilialId,
                    profissionalID: anamneseSubGrupoQuestaoOpcao.ProfissionalId,
                    anamneseGrupoID: anamneseSubGrupoQuestaoOpcao.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoId,
                    anamneseSubGrupoQuestaoID: anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoId,
                    texto: anamneseSubGrupoQuestaoOpcao.Texto,
                    ordem: anamneseSubGrupoQuestaoOpcao.Ordem,
                    ativo: anamneseSubGrupoQuestaoOpcao.Ativo
                );
                await Salvar(cAnamneseSubGrupoQuestaoOpcao);
                return (true, cAnamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoOpcaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAnamneseSubGrupoQuestaoOpcao.AtualizarPropriedades(
                    empresaID: anamneseSubGrupoQuestaoOpcao.EmpresaId,
                    filialID: anamneseSubGrupoQuestaoOpcao.FilialId,
                    profissionalID: anamneseSubGrupoQuestaoOpcao.ProfissionalId,
                    anamneseGrupoID: anamneseSubGrupoQuestaoOpcao.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoId,
                    anamneseSubGrupoQuestaoID: anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoId,
                    texto: anamneseSubGrupoQuestaoOpcao.Texto,
                    ordem: anamneseSubGrupoQuestaoOpcao.Ordem,
                    ativo: anamneseSubGrupoQuestaoOpcao.Ativo
                );
                await _anamneseSubGrupoQuestaoOpcaoRepo.Atualizar(cAnamneseSubGrupoQuestaoOpcao);
                await Atualizar(cAnamneseSubGrupoQuestaoOpcao);
            }

            return (false, anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoOpcaoId);
        }


        public async Task CriarParaImportacao(int anamneseSubGrupoQuestaoOpcaoID, Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID,
                                              int anamneseSubGrupoQuestaoID, string? texto, short ordem, bool? ativo)
        {
            var cAnamneseSubGrupoQuestaoOpcao = (await _anamneseSubGrupoQuestaoOpcaoRepo.Buscar(
                            x => x.AnamneseSubGrupoQuestaoOpcaoId == anamneseSubGrupoQuestaoOpcaoID)
                            ).FirstOrDefault();
            if (cAnamneseSubGrupoQuestaoOpcao == null)
            {
                cAnamneseSubGrupoQuestaoOpcao = AnamneseSubGrupoQuestaoOpcao.CriarParaImportacao(empresaID, filialID, profissionalID, anamneseGrupoID, anamneseSubGrupoID, anamneseSubGrupoQuestaoID, texto, ordem, ativo);
                await Salvar(cAnamneseSubGrupoQuestaoOpcao);
            }
            return;
        }

        public async Task Validar(int anamneseSubGrupoQuestaoOpcaoID)
        {
            var cAnamneseSubGrupoQuestaoOpcao = (await _anamneseSubGrupoQuestaoOpcaoRepo.Buscar(x => x.AnamneseSubGrupoQuestaoOpcaoId == anamneseSubGrupoQuestaoOpcaoID)).FirstOrDefault();
            if (cAnamneseSubGrupoQuestaoOpcao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Opção da questão do subgrupo de anamnese com ID {anamneseSubGrupoQuestaoOpcaoID} não encontrada."
                );
            }
        }
    }
}
