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
        /// Cria ou atualiza a fatura da empresa a partir do modelo informado
        /// </summary>
        /// <param name="empresaFatura"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int empresaFaturaId)> CriarOuAtualizar(CriarEmpresaFaturaInputModel empresaFatura, bool atualizaSeExistir);
    }
}
