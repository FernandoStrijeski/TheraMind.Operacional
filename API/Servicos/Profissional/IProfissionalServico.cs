using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Profissionais
{
    public interface IProfissionalServico
    {
        /// <summary>
        /// Busca o profissional pelo ID
        /// </summary>
        /// <param name="profissionalID"></param>
        /// <returns></returns>
        Task<Profissional>? BuscarPorID(Guid profissionalID);

        /// <summary>
        /// Busca os profissionais pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Profissional>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os profissionais
        /// </summary>
        /// <returns></returns>
        Task<List<Profissional>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo profissional
        /// </summary>
        /// <param name="profissional"></param>
        /// <returns></returns>
        Task<Profissional> Adicionar(Profissional profissional);

        /// <summary>
        /// Atualizar o profissional
        /// </summary>
        /// <param name="profissional"></param>
        /// <returns></returns>
        Task<Profissional> Atualizar(Profissional profissional);

        /// <summary>
        /// Remover o profissional
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);

    }
}
