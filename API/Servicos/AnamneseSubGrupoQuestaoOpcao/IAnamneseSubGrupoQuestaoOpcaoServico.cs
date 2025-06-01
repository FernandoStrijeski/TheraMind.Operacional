using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AnamneseSubGrupoQuestaoOpcoes
{
    public interface IAnamneseSubGrupoQuestaoOpcaoServico
    {
        /// <summary>
        /// Busca a opção da questão do subgrupo de anamnese pelo ID
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoOpcaoID"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestaoOpcao>? BuscarPorID(int anamneseSubGrupoQuestaoOpcaoID);

        /// <summary>
        /// Busca as opções das questões dos subgrupos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as opções das questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<AnamneseSubGrupoQuestaoOpcao>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza uma opção da questão do subgrupo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoOpcao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int anamneseSubGrupoQuestaoOpcaoId)> CriarOuAtualizar(CriarAnamneseSubGrupoQuestaoOpcaoInputModel anamneseSubGrupoQuestaoOpcao, bool atualizaSeExistir);
    }
}
