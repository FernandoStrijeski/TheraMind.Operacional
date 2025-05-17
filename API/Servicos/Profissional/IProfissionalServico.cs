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
        /// Cria ou atualiza um profissional a partir do modelo informado
        /// </summary>
        /// <param name="profissional"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid profissionalId)> CriarOuAtualizar(CriarProfissionalInputModel profissional, bool atualizaSeExistir);
    }
}
