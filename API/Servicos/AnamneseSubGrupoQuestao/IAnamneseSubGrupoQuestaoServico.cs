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
        /// Adicionar uma nova questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupoQuestao"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestao> Adicionar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao);

        /// <summary>
        /// Atualizar a questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupoQuestao"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestao> Atualizar(AnamneseSubGrupoQuestao anamneseSubGrupoQuestao);

        /// <summary>
        /// Remover a questão do subgrupo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);
    }
}
