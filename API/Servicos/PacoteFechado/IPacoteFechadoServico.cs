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
        /// Cria ou atualiza um pacote fechado a partir do modelo informado
        /// </summary>
        /// <param name="pacoteFechado"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int pacoteFechadoId)> CriarOuAtualizar(CriarPacoteFechadoInputModel pacoteFechado, bool atualizaSeExistir);
    }
}
