using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Usuarios
{
    public interface IUsuarioServico
    {
        /// <summary>
        /// Busca o usuário pelo ID
        /// </summary>
        /// <param name="usuarioID"></param>
        /// <returns></returns>
        Task<Usuario>? BuscarPorID(Guid usuarioID);

        /// <summary>
        /// Busca os usuários pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Usuario>> BuscarPorEmail(BuscarComEmailParametro parametro);

        /// <summary>
        /// Buscar todos os usuários
        /// </summary>
        /// <returns></returns>
        Task<List<Usuario>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um usuário a partir do modelo informado
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<bool> CriarOuAtualizar(CriarUsuarioInputModel usuario, bool atualizaSeExistir);
    }
}
