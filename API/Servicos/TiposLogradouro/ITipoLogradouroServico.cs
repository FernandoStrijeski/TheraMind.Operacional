using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.TiposLogradouros
{
    public interface ITipoLogradouroServico
    {
        /// <summary>
        /// Busca o tipo de logradouro pelo ID
        /// </summary>
        /// <param name="tipoLogradouroID"></param>
        /// <returns></returns>
        Task<TipoLogradouro>? BuscarPorID(string tipoLogradouroID);

        /// <summary>
        /// Busca os tipos de logradouros pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<TipoLogradouro>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de logradouros
        /// </summary>
        /// <returns></returns>
        Task<List<TipoLogradouro>> BuscarTodos();

        /// <summary>
        /// Adicionar uma novo tipo de logradouro
        /// </summary>
        /// <param name="tipoLogradouro"></param>
        /// <returns></returns>
        Task<TipoLogradouro> Adicionar(TipoLogradouro tipoLogradouro);

        /// <summary>
        /// Atualizar o tipo de logradouro
        /// </summary>
        /// <param name="tipoLogradouro"></param>
        /// <returns></returns>
        Task<TipoLogradouro> Atualizar(TipoLogradouro tipoLogradouro);

        /// <summary>
        /// Remover o tipo de logradouro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(string id);

    }
}
