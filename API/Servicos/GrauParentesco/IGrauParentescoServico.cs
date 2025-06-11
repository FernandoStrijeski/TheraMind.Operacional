using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.GrauParentescos
{
    public interface IGrauParentescoServico
    {
        /// <summary>
        /// Busca o grau de parentesco pelo ID
        /// </summary>
        /// <param name="grauParentescoID"></param>
        /// <returns></returns>
        Task<GrauParentesco>? BuscarPorID(int grauParentescoID);

        /// <summary>
        /// Busca os graus de parentescos pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<GrauParentesco>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os graus de parentescos
        /// </summary>
        /// <returns></returns>
        Task<List<GrauParentesco>> BuscarTodos();

        /// <summary>
        /// Adicionaa novo grau de parentesco
        /// </summary>
        /// <param name="grauParentesco"></param>
        /// <returns></returns>
        Task<GrauParentesco> Adicionar(GrauParentesco grauParentesco);

        /// <summary>
        /// Atualizar o grau de parentesco
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<GrauParentesco> Atualizar(GrauParentesco grauParentesco);

        /// <summary>
        /// Remover o grau de parentesco
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
