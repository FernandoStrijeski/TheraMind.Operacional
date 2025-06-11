using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.EmpresaFaturas
{
    public interface IEmpresaFaturaServico
    {
        /// <summary>
        /// Busca a fatura da empresa pelo ID
        /// </summary>
        /// <param name="empresaFaturaID"></param>
        /// <returns></returns>
        Task<EmpresaFatura>? BuscarPorID(int empresaFaturaID);

        /// <summary>
        /// Buscar todas as faturas da empresa
        /// </summary>
        /// <returns></returns>
        Task<List<EmpresaFatura>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova fatura da empresa
        /// </summary>
        /// <param name="empresaFatura"></param>
        /// <returns></returns>
        Task<EmpresaFatura> Adicionar(EmpresaFatura empresaFatura);

        /// <summary>
        /// Atualizar a fatura da empresa
        /// </summary>
        /// <param name="empresaFatura"></param>
        /// <returns></returns>
        Task<EmpresaFatura> Atualizar(EmpresaFatura empresaFatura);

        /// <summary>
        /// Remover a fatura da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);
    }
}
