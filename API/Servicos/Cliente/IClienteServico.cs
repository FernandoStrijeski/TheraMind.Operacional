using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Clientes
{
    public interface IClienteServico
    {
        /// <summary>
        /// Busca o cliente pelo ID
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        Task<Cliente>? BuscarPorID(Guid clienteID);

        /// <summary>
        /// Busca os clientes pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Cliente>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os clientes
        /// </summary>
        /// <returns></returns>
        Task<List<Cliente>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> Adicionar(Cliente cliente);

        /// <summary>
        /// Atualizar o cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        Task<Cliente> Atualizar(Cliente cliente);

        /// <summary>
        /// Remover o cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);

    }
}
