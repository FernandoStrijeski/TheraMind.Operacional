using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.EmpresasAssinaturas
{
    public interface IEmpresaAssinaturaServico
    {
        /// <summary>
        /// Busca a assinatura da empresa pelo ID
        /// </summary>
        /// <param name="assinaturaEmpresaID"></param>
        /// <returns></returns>
        Task<EmpresaAssinatura>? BuscarPorID(Guid assinaturaEmpresaID);

        /// <summary>
        /// Buscar todas as assinaturas das empresas
        /// </summary>
        /// <returns></returns>
        Task<List<EmpresaAssinatura>> BuscarTodos();

        /// <summary>
        /// Buscar todas as assinaturas da empresa selecionada
        /// </summary>
        /// <returns></returns>
        Task<List<EmpresaAssinatura>> BuscarPorIdEmpresa(Guid empresaID);

        /// <summary>
        /// Cria ou atualiza a assinatura da empresa a partir do modelo informado
        /// </summary>
        /// <param name="empresaAssinatura"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid empresaAssinaturaId)> CriarOuAtualizar(CriarEmpresaAssinaturaInputModel empresaAssinatura, bool atualizaSeExistir);
    }
}
