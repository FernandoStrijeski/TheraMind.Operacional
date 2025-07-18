using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.IdentidadesGeneros
{
    public interface IIdentidadeGeneroServico
    {
        /// <summary>
        /// Busca o tipo de identidade de gênero pelo ID
        /// </summary>
        /// <param name="identidadeGeneroID"></param>
        /// <returns></returns>
        Task<IdentidadeGenero>? BuscarPorID(int identidadeGeneroID);

        /// <summary>
        /// Busca os tipos de identidades de gêneros pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<IdentidadeGenero>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de identidades de gêneros
        /// </summary>
        /// <returns></returns>
        Task<List<IdentidadeGenero>> BuscarTodos();

        /// <summary>
        /// Adicionar uma identidade de gênero
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<IdentidadeGenero> Adicionar(IdentidadeGenero escolaridade);

        /// <summary>
        /// Atualizar a identidade de gênero
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<IdentidadeGenero> Atualizar(IdentidadeGenero escolaridade);

        /// <summary>
        /// Remover a identidade de gênero
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
