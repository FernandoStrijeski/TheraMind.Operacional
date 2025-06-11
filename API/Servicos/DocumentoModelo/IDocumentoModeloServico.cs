using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.DocumentosModelos
{
    public interface IDocumentoModeloServico
    {
        /// <summary>
        /// Busca o modelo de documento pelo ID
        /// </summary>
        /// <param name="documentoModeloID"></param>
        /// <returns></returns>
        Task<DocumentoModelo>? BuscarPorID(int documentoModeloID);

        /// <summary>
        /// Busca os modelos de documentos pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<DocumentoModelo>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os modelos de documentos
        /// </summary>
        /// <returns></returns>
        Task<List<DocumentoModelo>> BuscarTodos();

        /// <summary>
        /// Adicionar um modelo de documento
        /// </summary>
        /// <param name="documentoModelo"></param>
        /// <returns></returns>
        Task<DocumentoModelo> Adicionar(DocumentoModelo documentoModelo);

        /// <summary>
        /// Atualizar o modelo de documento
        /// </summary>
        /// <param name="documentoModelo"></param>
        /// <returns></returns>
        Task<DocumentoModelo> Atualizar(DocumentoModelo documentoModelo);

        /// <summary>
        /// Remover o modelo de documento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
