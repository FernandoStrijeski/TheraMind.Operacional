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
        /// Cria ou atualiza um modelo de documento a partir do modelo informado
        /// </summary>
        /// <param name="documentoModelo"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int documentoModeloId)> CriarOuAtualizar(CriarDocumentoModeloInputModel documentoModelo, bool atualizaSeExistir);
    }
}
