using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AnamneseSubGrupoQuestoes
{
    public interface IAnamneseSubGrupoQuestaoServico
    {
        /// <summary>
        /// Busca a questão do subgrupo de anamnese pelo ID
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoID"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestao>? BuscarPorID(int anamneseSubGrupoQuestaoID);

        /// <summary>
        /// Busca as questões dos subgrupos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AnamneseSubGrupoQuestao>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<AnamneseSubGrupoQuestao>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza uma questão do subgrupo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="anamneseSubGrupoQuestao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int anamneseSubGrupoQuestaoId)> CriarOuAtualizar(CriarAnamneseSubGrupoQuestaoInputModel anamneseSubGrupoQuestao, bool atualizaSeExistir);
    }
}
