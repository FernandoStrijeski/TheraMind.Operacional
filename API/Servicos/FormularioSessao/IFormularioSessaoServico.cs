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
        /// Adicionar um formulário de sessão
        /// </summary>
        /// <param name="formularioSessao"></param>
        /// <returns></returns>
        Task<FormularioSessao> Adicionar(FormularioSessao formularioSessao);

        /// <summary>
        /// Atualizar o formulário de sessão
        /// </summary>
        /// <param name="formularioSessao"></param>
        /// <returns></returns>
        Task<FormularioSessao> Atualizar(FormularioSessao formularioSessao);

        /// <summary>
        /// Remover o formulário de sessão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
