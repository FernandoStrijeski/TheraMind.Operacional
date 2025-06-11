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
        /// Adicionar um novo subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupo"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupo> Adicionar(AnamneseSubGrupo anamneseSubGrupo);

        /// <summary>
        /// Atualizar o subgrupo de anamnese
        /// </summary>
        /// <param name="anamneseSubGrupo"></param>
        /// <returns></returns>
        Task<AnamneseSubGrupo> Atualizar(AnamneseSubGrupo anamneseSubGrupo);

        /// <summary>
        /// Remover o subgrupo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
