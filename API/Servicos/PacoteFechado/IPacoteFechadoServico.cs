using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.PacotesFechados
{
    public interface IPacoteFechadoServico
    {
        /// <summary>
        /// Busca o pacote fechado pelo ID
        /// </summary>
        /// <param name="pacoteFechadoID"></param>
        /// <returns></returns>
        Task<PacoteFechado>? BuscarPorID(int pacoteFechadoID);

        /// <summary>
        /// Buscar todos os pacotes fechados
        /// </summary>
        /// <returns></returns>
        Task<List<PacoteFechado>> BuscarTodos();

        /// <summary>
        /// Adicionar um pacote fechado
        /// </summary>
        /// <param name="pacoteFechado"></param>
        /// <returns></returns>
        Task<PacoteFechado> Adicionar(PacoteFechado pacoteFechado);

        /// <summary>
        /// Atualizar o pacote fechado
        /// </summary>
        /// <param name="pacoteFechado"></param>
        /// <returns></returns>
        Task<PacoteFechado> Atualizar(PacoteFechado pacoteFechado);

        /// <summary>
        /// Remover o pacote fechado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
