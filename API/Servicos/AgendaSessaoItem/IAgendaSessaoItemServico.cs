using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AgendasSessaoItens
{
    public interface IAgendaSessaoItemServico
    {
        /// <summary>
        /// Busca o item da sessão na agenda pelo ID
        /// </summary>
        /// <param name="agendaSessaoItemID"></param>
        /// <returns></returns>
        Task<AgendaSessaoItem>? BuscarPorID(int agendaSessaoItemID);

        /// <summary>
        /// Busca os itens das sessões na agenda pelo nome do cliente ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AgendaSessaoItem>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os itens da sessão
        /// </summary>
        /// <returns></returns>
        Task<List<AgendaSessaoItem>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo item da sessão na agenda
        /// </summary>
        /// <param name="agendaSessaoItem"></param>
        /// <returns></returns>
        Task<AgendaSessaoItem> Adicionar(AgendaSessaoItem agendaSessaoItem);

        /// <summary>
        /// Atualizar o item da sessão na agenda
        /// </summary>
        /// <param name="agendaSessaoItem"></param>
        /// <returns></returns>
        Task<AgendaSessaoItem> Atualizar(AgendaSessaoItem agendaSessaoItem);

        /// <summary>
        /// Remover o item da sessão na agenda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
