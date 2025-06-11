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
        /// Adicionar um novo serviço
        /// </summary>
        /// <param name="servico"></param>
        /// <returns></returns>
        Task<Servico> Adicionar(Servico servico);

        /// <summary>
        /// Atualizar o serviço
        /// </summary>
        /// <param name="servico"></param>
        /// <returns></returns>
        Task<Servico> Atualizar(Servico servico);

        /// <summary>
        /// Remover o serviço
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);
    }
}
