using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.TiposEtnias
{
    public interface ITipoEtniaServico
    {
        /// <summary>
        /// Busca o tipo de etnia pelo ID
        /// </summary>
        /// <param name="tipoEtniaID"></param>
        /// <returns></returns>
        Task<TipoEtnia>? BuscarPorID(int tipoEtniaID);

        /// <summary>
        /// Busca os tipos de etnias pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<TipoEtnia>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de etnias
        /// </summary>
        /// <returns></returns>
        Task<List<TipoEtnia>> BuscarTodos();
    }
}
