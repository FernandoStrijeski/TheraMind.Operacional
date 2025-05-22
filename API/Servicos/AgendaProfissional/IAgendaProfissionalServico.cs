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
        /// Cria ou atualiza a agenda profissional a partir do modelo informado
        /// </summary>
        /// <param name="agendaProfissional"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int agendaProfissionalId)> CriarOuAtualizar(CriarAgendaProfissionalInputModel agendaProfissional, bool atualizaSeExistir);
    }
}
