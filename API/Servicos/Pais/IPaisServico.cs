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
        /// Adicionar um país
        /// </summary>
        /// <param name="pais"></param>
        /// <returns></returns>
        Task<Pais> Adicionar(Pais pais);

        /// <summary>
        /// Atualizar o país
        /// </summary>
        /// <param name="pais"></param>
        /// <returns></returns>
        Task<Pais> Atualizar(Pais pais);

        /// <summary>
        /// Remover o país
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

        /// <summary>
        /// Buscar todos os países
        /// </summary>
        /// <returns></returns>
        Task<List<Pais>> BuscarTodos();
    }
}
