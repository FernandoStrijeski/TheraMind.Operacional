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
        /// Cria ou atualiza um cliente a partir do modelo informado
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid clienteId)> CriarOuAtualizar(CriarClienteInputModel cliente, bool atualizaSeExistir);
    }
}
