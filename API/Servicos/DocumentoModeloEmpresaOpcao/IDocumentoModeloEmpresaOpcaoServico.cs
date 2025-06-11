using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.DocumentosModelosEmpresasOpcoes
{
    public interface IDocumentoModeloEmpresaOpcaoServico
    {
        /// <summary>
        /// Busca a opção para o modelo de documento da empresa pelo ID
        /// </summary>
        /// <param name="documentoModeloEmpresaOpcaoID"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresaOpcao>? BuscarPorID(int documentoModeloEmpresaOpcaoID);

        /// <summary>
        /// Buscar todas as opções para os modelos de documentos da empresa
        /// </summary>
        /// <returns></returns>
        Task<List<DocumentoModeloEmpresaOpcao>> BuscarTodos();

        /// <summary>
        /// Adicionar uma opção do modelo de documento da empresa
        /// </summary>
        /// <param name="documentoModeloEmpresaOpcao"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresaOpcao> Adicionar(DocumentoModeloEmpresaOpcao documentoModeloEmpresaOpcao);

        /// <summary>
        /// Atualizar a opção do modelo de documento da empresa
        /// </summary>
        /// <param name="documentoModeloEmpresaOpcao"></param>
        /// <returns></returns>
        Task<DocumentoModeloEmpresaOpcao> Atualizar(DocumentoModeloEmpresaOpcao documentoModeloEmpresaOpcao);

        /// <summary>
        /// Remover a opção do modelo de documento da empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
