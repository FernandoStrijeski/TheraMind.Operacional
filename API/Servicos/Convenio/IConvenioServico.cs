using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Convenios
{
    public interface IConvenioServico
    {
        /// <summary>
        /// Busca o convênio pelo ID
        /// </summary>
        /// <param name="convenioID"></param>
        /// <returns></returns>
        Task<Convenio>? BuscarPorID(int convenioID);

        /// <summary>
        /// Busca os convênios pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Convenio>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os convênios
        /// </summary>
        /// <returns></returns>
        Task<List<Convenio>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um convênio a partir do modelo informado
        /// </summary>
        /// <param name="convenio"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int convenioId)> CriarOuAtualizar(CriarConvenioInputModel convenio, bool atualizaSeExistir);
    }
}
