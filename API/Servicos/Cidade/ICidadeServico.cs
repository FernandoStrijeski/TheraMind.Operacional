using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Cidades
{
    public interface ICidadeServico
    {
        /// <summary>
        /// Busca a cidade pelo ID
        /// </summary>
        /// <param name="cidadeID"></param>
        /// <returns></returns>
        Task<Cidade>? BuscarPorID(int cidadeID);

        /// <summary>
        /// Busca as cidades pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Cidade>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as cidades
        /// </summary>
        /// <returns></returns>
        Task<List<Cidade>> BuscarTodos();
    }
}
