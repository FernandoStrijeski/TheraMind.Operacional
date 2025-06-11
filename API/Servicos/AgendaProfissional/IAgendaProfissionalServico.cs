using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AgendasProfissionais
{
    public interface IAgendaProfissionalServico
    {
        /// <summary>
        /// Busca a agenda do profissional pelo ID
        /// </summary>
        /// <param name="agendaProfissionalID"></param>
        /// <returns></returns>
        Task<AgendaProfissional>? BuscarPorID(int agendaProfissionalID);

        /// <summary>
        /// Buscar todos as agendas profissionais
        /// </summary>
        /// <returns></returns>
        Task<List<AgendaProfissional>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova agenda do profissional
        /// </summary>
        /// <param name="agendaProfissional"></param>
        /// <returns></returns>
        Task<AgendaProfissional> Adicionar(AgendaProfissional agendaProfissional);

        /// <summary>
        /// Atualizar a agenda do profissional
        /// </summary>
        /// <param name="agendaProfissional"></param>
        /// <returns></returns>
        Task<AgendaProfissional> Atualizar(AgendaProfissional agendaProfissional);

        /// <summary>
        /// Remover a agenda do profissional
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
