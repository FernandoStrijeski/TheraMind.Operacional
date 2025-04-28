using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Nacionalidades
{
    public interface INacionalidadeServico
    {
        /// <summary>
        /// Busca o tipo de nacionalidade pelo ID
        /// </summary>
        /// <param name="nacionalidadeID"></param>
        /// <returns></returns>
        Task<Nacionalidade>? BuscarPorID(int nacionalidadeID);

        /// <summary>
        /// Busca os tipos de nacionalidades pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Nacionalidade>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de nacionalidades
        /// </summary>
        /// <returns></returns>
        Task<List<Nacionalidade>> BuscarTodos();
    }
}
