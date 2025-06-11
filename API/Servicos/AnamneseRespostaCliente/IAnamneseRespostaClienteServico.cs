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
        /// Adicionar uma nova resposta da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseRespostaCliente"></param>
        /// <returns></returns>
        Task<AnamneseRespostaCliente> Adicionar(AnamneseRespostaCliente anamneseRespostaCliente);

        /// <summary>
        /// Atualizar a resposta da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseRespostaCliente"></param>
        /// <returns></returns>
        Task<AnamneseRespostaCliente> Atualizar(AnamneseRespostaCliente anamneseRespostaCliente);

        /// <summary>
        /// Remover a resposta da questão do subgrupo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
