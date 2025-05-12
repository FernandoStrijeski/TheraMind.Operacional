using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Paises
{
    public interface IPaisServico
    {
        /// <summary>
        /// Busca o país pelo ID
        /// </summary>
        /// <param name="paisID"></param>
        /// <returns></returns>
        Task<Pais>? BuscarPorID(int paisID);

        /// <summary>
        /// Busca os países pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Pais>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os países
        /// </summary>
        /// <returns></returns>
        Task<List<Pais>> BuscarTodos();
    }
}
