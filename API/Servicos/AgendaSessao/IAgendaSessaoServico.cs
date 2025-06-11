using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AgendasSessoes
{
    public interface IAgendaSessaoServico
    {
        /// <summary>
        /// Busca a sessão na agenda pelo ID
        /// </summary>
        /// <param name="agendaSessaoID"></param>
        /// <returns></returns>
        Task<AgendaSessao>? BuscarPorID(Guid agendaSessaoID);

        /// <summary>
        /// Busca as sessões na agenda pelo nome do cliente ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AgendaSessao>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas sessões da agenda
        /// </summary>
        /// <returns></returns>
        Task<List<AgendaSessao>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova sessão na agenda
        /// </summary>
        /// <param name="agendaSessao"></param>
        /// <returns></returns>
        Task<AgendaSessao> Adicionar(AgendaSessao agendaSessao);

        /// <summary>
        /// Atualizar a sessão na agenda
        /// </summary>
        /// <param name="agendaSessao"></param>
        /// <returns></returns>
        Task<AgendaSessao> Atualizar(AgendaSessao agendaSessao);

        /// <summary>
        /// Remover a sessão na agenda
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);
    }
}
