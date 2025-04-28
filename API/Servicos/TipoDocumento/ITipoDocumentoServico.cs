using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.TiposDocumentos
{
    public interface ITipoDocumentoServico
    {
        /// <summary>
        /// Busca o tipo de documento pelo ID
        /// </summary>
        /// <param name="tipoDocumentoID"></param>
        /// <returns></returns>
        Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID);

        /// <summary>
        /// Busca os tipos de documentos pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<TipoDocumento>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de documentos
        /// </summary>
        /// <returns></returns>
        Task<List<TipoDocumento>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um tipo de documento a partir do modelo informado
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<bool> CriarOuAtualizar(CriarTipoDocumentoInputModel tipoDocumento, bool atualizaSeExistir);
    }
}
