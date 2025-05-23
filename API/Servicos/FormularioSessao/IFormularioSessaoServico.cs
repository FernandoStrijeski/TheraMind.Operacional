using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.FormulariosSessoes
{
    public interface IFormularioSessaoServico
    {
        /// <summary>
        /// Busca o formulário da sessão pelo ID
        /// </summary>
        /// <param name="formularioSessaoID"></param>
        /// <returns></returns>
        Task<FormularioSessao>? BuscarPorID(int formularioSessaoID);

        /// <summary>
        /// Busca os formulários da sessão pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<FormularioSessao>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os formulários da sessão
        /// </summary>
        /// <returns></returns>
        Task<List<FormularioSessao>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um formulário da sessão a partir do modelo informado
        /// </summary>
        /// <param name="formularioSessao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int formularioSessaoId)> CriarOuAtualizar(CriarFormularioSessaoInputModel formularioSessao, bool atualizaSeExistir);
    }
}
