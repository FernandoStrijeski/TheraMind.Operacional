using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Servicos
{
    public interface IServicoServico
    {
        /// <summary>
        /// Busca o serviço pelo ID
        /// </summary>
        /// <param name="servicoID"></param>
        /// <returns></returns>
        Task<Servico>? BuscarPorID(int servicoID);

        /// <summary>
        /// Busca os serviços pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Servico>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os serviços
        /// </summary>
        /// <returns></returns>
        Task<List<Servico>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um serviço a partir do modelo informado
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int servicoId)> CriarOuAtualizar(CriarServicoInputModel servico, bool atualizaSeExistir);
    }
}
