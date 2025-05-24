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
        /// Cria ou atualiza um item da sessão na agenda a partir do modelo informado
        /// </summary>
        /// <param name="agendaSessaoItem"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int agendaSessaoItemId)> CriarOuAtualizar(CriarAgendaSessaoItemInputModel agendaSessaoItem, bool atualizaSeExistir);
    }
}
