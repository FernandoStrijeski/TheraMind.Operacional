using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AnamneseSubGrupos
{
    public interface IAnamneseSubGrupoServico
    {
        /// <summary>
        /// Busca o subgrupo de anamnese pelo ID
        /// </summary>
        /// <param name="anamneseSubGrupoID"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupo>? BuscarPorID(int anamneseSubGrupoID);

        /// <summary>
        /// Busca os subgrupos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<AnamneseSubGrupo>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<AnamneseSubGrupo>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um subgrupo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="anamneseSubGrupo"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int anamneseSubGrupoId)> CriarOuAtualizar(CriarAnamneseSubGrupoInputModel anamneseSubGrupo, bool atualizaSeExistir);
    }
}
