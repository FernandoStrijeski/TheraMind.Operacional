using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.ProfissionaisAcessos
{
    public interface IProfissionalAcessoServico
    {
        /// <summary>
        /// Busca o acesso do profissional pelo ID
        /// </summary>
        /// <param name="profissionalAcessoID"></param>
        /// <returns></returns>
        Task<ProfissionalAcesso>? BuscarPorID(int profissionalAcessoID);

        /// <summary>
        /// Busca o acesso do profissional pelo ID do profissional
        /// </summary>
        /// <param name="profissionalID"></param>
        /// <returns></returns>
        Task<List<ProfissionalAcesso>> BuscarPorIDProfissional(Guid profissionalID);
        
        /// <summary>
        /// Buscar todos os acessos 
        /// </summary>
        /// <returns></returns>
        Task<List<ProfissionalAcesso>> BuscarTodos();

        /// <summary>
        /// Adicionar um novo acesso do profissional
        /// </summary>
        /// <param name="profissionalAcesso"></param>
        /// <returns></returns>
        Task<ProfissionalAcesso> Adicionar(ProfissionalAcesso profissionalAcesso);

        /// <summary>
        /// Atualizar o acesso do profissional
        /// </summary>
        /// <param name="profissionalAcesso"></param>
        /// <returns></returns>
        Task<ProfissionalAcesso> Atualizar(ProfissionalAcesso profissionalAcesso);

        /// <summary>
        /// Remover o acesso do profissional
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);
        
    }
}
