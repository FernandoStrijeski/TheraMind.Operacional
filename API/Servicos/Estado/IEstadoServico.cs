using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Estados
{
    public interface IEstadoServico
    {
        /// <summary>
        /// Busca o estado pelo UF
        /// </summary>
        /// <param name="paisID"></param>
        /// <returns></returns>
        Task<Estado>? BuscarPorID(string uf);

        /// <summary>
        /// Busca os estados pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Estado>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os estados
        /// </summary>
        /// <returns></returns>
        Task<List<Estado>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        Task<Estado> Adicionar(Estado estado);

        /// <summary>
        /// Atualizar o estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        Task<Estado> Atualizar(Estado estado);

        /// <summary>
        /// Remover o estado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(string id);

    }
}
