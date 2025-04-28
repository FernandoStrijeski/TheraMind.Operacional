using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.OrientacoesSexuais
{
    public interface IOrientacaoSexualServico
    {
        /// <summary>
        /// Busca o tipo de orientação sexual pelo ID
        /// </summary>
        /// <param name="orientacaoSexualID"></param>
        /// <returns></returns>
        Task<OrientacaoSexual>? BuscarPorID(int orientacaoSexualID);

        /// <summary>
        /// Busca os tipos de orientações sexuais pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<OrientacaoSexual>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de orientações sexuais
        /// </summary>
        /// <returns></returns>
        Task<List<OrientacaoSexual>> BuscarTodos();
    }
}
