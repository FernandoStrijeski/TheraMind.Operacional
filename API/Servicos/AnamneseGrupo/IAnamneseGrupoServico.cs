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
        /// Adicionar um novo grupo de anamnese
        /// </summary>
        /// <param name="anamneseGrupo"></param>
        /// <returns></returns>
        Task<AnamneseGrupo> Adicionar(AnamneseGrupo anamneseGrupo);

        /// <summary>
        /// Atualizar o grupo de anamnese
        /// </summary>
        /// <param name="anamneseGrupo"></param>
        /// <returns></returns>
        Task<AnamneseGrupo> Atualizar(AnamneseGrupo anamneseGrupo);

        /// <summary>
        /// Remover o grupo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
