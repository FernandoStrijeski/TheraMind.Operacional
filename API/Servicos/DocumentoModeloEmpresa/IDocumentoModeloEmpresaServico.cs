using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.DocumentosModelosEmpresas
{
    public interface IDocumentoModeloEmpresaServico
    {
        /// <summary>
        /// Busca o modelo de documento da empresa pelo ID
        /// </summary>
        /// <param name="documentoModeloEmpresaID"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresa>? BuscarPorID(int documentoModeloEmpresaID);

        /// <summary>
        /// Busca os modelos de documentos da empresa pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<DocumentoModeloEmpresa>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os modelos de documentos da empresa
        /// </summary>
        /// <returns></returns>
        Task<List<DocumentoModeloEmpresa>> BuscarTodos();

        /// <summary>
        /// Adicionar um modelo de documento da empresa
        /// </summary>
        /// <param name="documentoModeloEmpresa"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresa> Adicionar(DocumentoModeloEmpresa documentoModeloEmpresa);

        /// <summary>
        /// Atualizar o modelo de documento da empresa
        /// </summary>
        /// <param name="documentoModeloEmpresa"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresa> Atualizar(DocumentoModeloEmpresa documentoModeloEmpresa);

        /// <summary>
        /// Remover o modelo de documento da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
