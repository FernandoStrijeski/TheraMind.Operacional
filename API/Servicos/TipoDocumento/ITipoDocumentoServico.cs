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
        /// Adicionar uma nova escolaridade
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<TipoDocumento> Adicionar(TipoDocumento escolaridade);

        /// <summary>
        /// Atualizar a escolaridade
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<TipoDocumento> Atualizar(TipoDocumento escolaridade);

        /// <summary>
        /// Remover a escolaridade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
