using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Planos
{
    public interface IPlanoServico
    {
        /// <summary>
        /// Busca o plano pelo ID
        /// </summary>
        /// <param name="planoID"></param>
        /// <returns></returns>
        Task<Plano>? BuscarPorID(Guid planoID);

        /// <summary>
        /// Busca os planos pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Plano>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os planos
        /// </summary>
        /// <returns></returns>
        Task<List<Plano>> BuscarTodos();

        /// <summary>
        /// Adicionar um plano
        /// </summary>
        /// <param name="plano"></param>
        /// <returns></returns>
        Task<Plano> Adicionar(Plano plano);

        /// <summary>
        /// Atualizar o plano
        /// </summary>
        /// <param name="plano"></param>
        /// <returns></returns>
        Task<Plano> Atualizar(Plano plano);

        /// <summary>
        /// Remover o plano
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);
    }
}
