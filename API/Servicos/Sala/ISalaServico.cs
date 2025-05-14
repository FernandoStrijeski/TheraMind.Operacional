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
        /// Cria ou atualiza uma sala a partir do modelo informado
        /// </summary>
        /// <param name="sala"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<bool> CriarOuAtualizar(CriarSalaInputModel sala, bool atualizaSeExistir);
    }
}
