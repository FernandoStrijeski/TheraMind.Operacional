using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.FormularioSessaoCampos
{
    public interface IFormularioSessaoCampoServico
    {
        /// <summary>
        /// Busca o campo do formulário da sessão pelo ID
        /// </summary>
        /// <param name="formularioSessaoCampoID"></param>
        /// <returns></returns>
        Task<FormularioSessaoCampo>? BuscarPorID(int formularioSessaoCampoID);

        /// <summary>
        /// Busca os campos dos formulários da sessão pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<FormularioSessaoCampo>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos campos dos formulários da sessão
        /// </summary>
        /// <returns></returns>
        Task<List<FormularioSessaoCampo>> BuscarTodos();

        /// <summary>
        /// Adicionar um campo do formulário da sessão
        /// </summary>
        /// <param name="formularioSessaoCampo"></param>
        /// <returns></returns>
        Task<FormularioSessaoCampo> Adicionar(FormularioSessaoCampo formularioSessaoCampo);

        /// <summary>
        /// Atualizar o campo do formulário da sessão
        /// </summary>
        /// <param name="formularioSessaoCampo"></param>
        /// <returns></returns>
        Task<FormularioSessaoCampo> Atualizar(FormularioSessaoCampo formularioSessaoCampo);

        /// <summary>
        /// Remover o campo do formulário da sessão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
