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
        /// Adicionar um novo convênio
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        Task<Convenio> Adicionar(Convenio convenio);

        /// <summary>
        /// Atualizar o convênio
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        Task<Convenio> Atualizar(Convenio convenio);

        /// <summary>
        /// Remover o convênio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
