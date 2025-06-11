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
        /// Adicionar uma assinatura da empresa
        /// </summary>
        /// <param name="assinaturaEmpresa"></param>
        /// <returns></returns>
        Task<EmpresaAssinatura> Adicionar(EmpresaAssinatura assinaturaEmpresa);

        /// <summary>
        /// Atualizar a assinatura da empresa
        /// </summary>
        /// <param name="assinaturaEmpresa"></param>
        /// <returns></returns>
        Task<EmpresaAssinatura> Atualizar(EmpresaAssinatura assinaturaEmpresa);

        /// <summary>
        /// Remover a assinatura da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);
    }
}
