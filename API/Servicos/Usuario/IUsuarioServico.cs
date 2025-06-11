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
        /// Adicionar um novo usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> Adicionar(Usuario usuario);

        /// <summary>
        /// Atualizar o usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        Task<Usuario> Atualizar(Usuario usuario);

        /// <summary>
        /// Remover o usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);

    }
}
