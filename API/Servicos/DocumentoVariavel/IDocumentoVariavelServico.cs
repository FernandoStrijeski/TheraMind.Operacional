using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.DocumentosVariaveis
{
    public interface IDocumentoVariavelServico
    {
        /// <summary>
        /// Busca a variável do documento pelo ID
        /// </summary>
        /// <param name="documentoVariavelID"></param>
        /// <returns></returns>
        Task<DocumentoVariavel>? BuscarPorID(int documentoVariavelID);

        /// <summary>
        /// Busca as variáveis dos documentos pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<DocumentoVariavel>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as variáveis de documentos
        /// </summary>
        /// <returns></returns>
        Task<List<DocumentoVariavel>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova variável do documento
        /// </summary>
        /// <param name="documentoVariavel"></param>
        /// <returns></returns>
        Task<DocumentoVariavel> Adicionar(DocumentoVariavel documentoVariavel);

        /// <summary>
        /// Atualizar a variável do documento
        /// </summary>
        /// <param name="documentoVariavel"></param>
        /// <returns></returns>
        Task<DocumentoVariavel> Atualizar(DocumentoVariavel documentoVariavel);

        /// <summary>
        /// Remover a variável do documento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
