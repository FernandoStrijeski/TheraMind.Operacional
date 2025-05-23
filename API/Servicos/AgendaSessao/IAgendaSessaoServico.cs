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
        /// Buscar todos os modelos de documentos da empresa
        /// </summary>
        /// <returns></returns>
        Task<List<AgendaSessao>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza uma sessão na agenda a partir do modelo informado
        /// </summary>
        /// <param name="agendaSessao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid agendaSessaoId)> CriarOuAtualizar(CriarAgendaSessaoInputModel agendaSessao, bool atualizaSeExistir);
    }
}
