using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AnamneseRespostaClientes
{
    public interface IAnamneseRespostaClienteServico
    {
        /// <summary>
        /// Busca a resposta da questão do subgrupo de anamnese pelo ID
        /// </summary>
        /// <param name="anamneseSubGrupoQuestaoID"></param>
        /// <returns></returns>
        Task<AnamneseRespostaCliente>? BuscarPorID(int anamneseSubGrupoQuestaoID);

        /// <summary>
        /// Busca as resposdas das questões dos subgrupos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AnamneseRespostaCliente>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as respostas das questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<AnamneseRespostaCliente>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza uma resposta para a questão do subgrupo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="anamneseRespostaCliente"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int anamneseSubGrupoQuestaoId)> CriarOuAtualizar(CriarAnamneseRespostaClienteInputModel anamneseRespostaCliente, bool atualizaSeExistir);
    }
}
