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

        /// <summary>
        /// Adicionar um tipo de etnia
        /// </summary>
        /// <param name="tipoEtnia"></param>
        /// <returns></returns>
        Task<TipoEtnia> Adicionar(TipoEtnia tipoEtnia);

        /// <summary>
        /// Atualizar o tipo de etnia
        /// </summary>
        /// <param name="tipoEtnia"></param>
        /// <returns></returns>
        Task<TipoEtnia> Atualizar(TipoEtnia tipoEtnia);

        /// <summary>
        /// Remover o tipo de etnia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
