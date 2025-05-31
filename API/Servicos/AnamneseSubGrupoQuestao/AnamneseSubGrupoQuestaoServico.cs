using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao)
        {
            await _anamneseSubGrupoQuestaoRepo.Adicionar(anamneseSubGrupoQuestao);
            await Comitar();
        }

        private async Task Atualizar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao)
        {
            await _anamneseSubGrupoQuestaoRepo.Atualizar(anamneseSubGrupoQuestao);
            await Comitar();
        }

        public async Task<(bool criado, int anamneseSubGrupoQuestaoId)> CriarOuAtualizar(CriarAnamneseSubGrupoQuestaoInputModel anamneseSubGrupoQuestao, bool atualizaSeExistir)
        {
            var cAnamneseSubGrupoQuestao = (await _anamneseSubGrupoQuestaoRepo.Buscar(
                x => x.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoId
            )).FirstOrDefault();

            if (cAnamneseSubGrupoQuestao == null)
            {
                cAnamneseSubGrupoQuestao = AnamneseSubGrupoQuestao.CriarParaImportacao(
                    empresaID: anamneseSubGrupoQuestao.EmpresaId,
                    filialID: anamneseSubGrupoQuestao.FilialId,
                    profissionalID: anamneseSubGrupoQuestao.ProfissionalId,
                    anamneseGrupoID: anamneseSubGrupoQuestao.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseSubGrupoQuestao.AnamneseSubGrupoId,
                    titulo: anamneseSubGrupoQuestao.Titulo,
                    tipoOpcao: anamneseSubGrupoQuestao.TipoOpcao,
                    ordem: anamneseSubGrupoQuestao.Ordem,
                    ativo: anamneseSubGrupoQuestao.Ativo
                );
                await Salvar(cAnamneseSubGrupoQuestao);
                return (true, cAnamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAnamneseSubGrupoQuestao.AtualizarPropriedades(
                    empresaID: anamneseSubGrupoQuestao.EmpresaId,
                    filialID: anamneseSubGrupoQuestao.FilialId,
                    profissionalID: anamneseSubGrupoQuestao.ProfissionalId,
                    anamneseGrupoID: anamneseSubGrupoQuestao.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseSubGrupoQuestao.AnamneseSubGrupoId,
                    titulo: anamneseSubGrupoQuestao.Titulo,
                    tipoOpcao: anamneseSubGrupoQuestao.TipoOpcao,
                    ordem: anamneseSubGrupoQuestao.Ordem,
                    ativo: anamneseSubGrupoQuestao.Ativo
                );
                await _anamneseSubGrupoQuestaoRepo.Atualizar(cAnamneseSubGrupoQuestao);
                await Atualizar(cAnamneseSubGrupoQuestao);
            }

            return (false, anamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoId);
        }


        public async Task CriarParaImportacao(int anamneseSubGrupoQuestaoID, Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            var cAnamneseSubGrupoQuestao = (await _anamneseSubGrupoQuestaoRepo.Buscar(
                            x => x.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID)
                            ).FirstOrDefault();
            if (cAnamneseSubGrupoQuestao == null)
            {
                cAnamneseSubGrupoQuestao = AnamneseSubGrupoQuestao.CriarParaImportacao(empresaID, filialID, profissionalID, anamneseGrupoID, anamneseSubGrupoID, titulo, tipoOpcao, ordem, ativo);
                await Salvar(cAnamneseSubGrupoQuestao);
            }
            return;
        }

        public async Task Validar(int anamneseSubGrupoQuestaoID)
        {
            var cAnamneseSubGrupoQuestao = (await _anamneseSubGrupoQuestaoRepo.Buscar(x => x.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID)).FirstOrDefault();
            if (cAnamneseSubGrupoQuestao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Questão do ubgrupo de anamnese com ID {anamneseSubGrupoQuestaoID} não encontrada."
                );
            }
        }
    }
}
