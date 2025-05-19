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
        /// Cria ou atualiza um acesso do profissional a partir do modelo informado
        /// </summary>
        /// <param name="profissionalAcesso"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int profissionalAcessoId)> CriarOuAtualizar(CriarProfissionalAcessoInputModel profissionalAcesso, bool atualizaSeExistir);
        
    }
}
