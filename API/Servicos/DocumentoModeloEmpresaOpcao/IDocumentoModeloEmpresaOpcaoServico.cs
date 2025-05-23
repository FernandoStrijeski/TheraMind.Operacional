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
        /// Cria ou atualiza uma opção para o modelo de documento da empresa a partir do modelo informado
        /// </summary>
        /// <param name="documentoModeloEmpresaOpcao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int documentoModeloEmpresaOpcaoId)> CriarOuAtualizar(CriarDocumentoModeloEmpresaOpcaoInputModel documentoModeloEmpresaOpcao, bool atualizaSeExistir);
    }
}
