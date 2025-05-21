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
        /// Cria ou atualiza uma variável de documento a partir do modelo informado
        /// </summary>
        /// <param name="documentoVariavel"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int documentoVariavelId)> CriarOuAtualizar(CriarDocumentoVariavelInputModel documentoVariavel, bool atualizaSeExistir);
    }
}
