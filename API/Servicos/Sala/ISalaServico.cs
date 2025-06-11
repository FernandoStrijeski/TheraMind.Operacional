using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Salas
{
    public interface ISalaServico
    {
        /// <summary>
        /// Busca a sala pelo ID
        /// </summary>
        /// <param name="salaID"></param>
        /// <returns></returns>
        Task<Sala>? BuscarPorID(string salaID);

        /// <summary>
        /// Busca as salas pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Sala>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as salas
        /// </summary>
        /// <returns></returns>
        Task<List<Sala>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova sala
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        Task<Sala> Adicionar(Sala sala);

        /// <summary>
        /// Atualizar a sala
        /// </summary>
        /// <param name="sala"></param>
        /// <returns></returns>
        Task<Sala> Atualizar(Sala sala);

        /// <summary>
        /// Remover a sala
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(string id);

    }
}
