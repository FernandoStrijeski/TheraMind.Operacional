using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AnamneseGrupos
{
    public interface IAnamneseGrupoServico
    {
        /// <summary>
        /// Busca o grupo de anamnese pelo ID
        /// </summary>
        /// <param name="anamneseGrupoID"></param>
        /// <returns></returns>
        Task<AnamneseGrupo>? BuscarPorID(int anamneseGrupoID);

        /// <summary>
        /// Busca os grupos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AnamneseGrupo>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os grupos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<AnamneseGrupo>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um grupo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="anamneseGrupo"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int anamneseGrupoId)> CriarOuAtualizar(CriarAnamneseGrupoInputModel anamneseGrupo, bool atualizaSeExistir);
    }
}
