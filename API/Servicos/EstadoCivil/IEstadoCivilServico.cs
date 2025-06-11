using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.EstadosCivis
{
    public interface IEstadoCivilServico
    {
        /// <summary>
        /// Busca o tipo de estado civil pelo ID
        /// </summary>
        /// <param name="estadoCivilID"></param>
        /// <returns></returns>
        Task<EstadoCivil>? BuscarPorID(string estadoCivilID);

        /// <summary>
        /// Busca os tipos de estados civis pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<EstadoCivil>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de estados civis
        /// </summary>
        /// <returns></returns>
        Task<List<EstadoCivil>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo estado civil
        /// </summary>
        /// <param name="estadoCivil"></param>
        /// <returns></returns>
        Task<EstadoCivil> Adicionar(EstadoCivil estadoCivil);

        /// <summary>
        /// Atualizar o estado civil
        /// </summary>
        /// <param name="estadoCivil"></param>
        /// <returns></returns>
        Task<EstadoCivil> Atualizar(EstadoCivil estadoCivil);

        /// <summary>
        /// Remover o estado civil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(string id);

    }
}
