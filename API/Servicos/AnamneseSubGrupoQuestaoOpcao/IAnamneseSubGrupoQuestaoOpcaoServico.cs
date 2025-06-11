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
        /// Adicionar uma nova opção da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoOpcao"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestaoOpcao> Adicionar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao);

        /// <summary>
        /// Atualizar a opção da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoOpcao"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupoQuestaoOpcao> Atualizar(AnamneseSubGrupoQuestaoOpcao anamneseSubGrupoQuestaoOpcao);

        /// <summary>
        /// Remover a opção da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
