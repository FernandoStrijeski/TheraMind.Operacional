using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.ModelosAnamneseSG
{
    public interface IModeloAnamneseSgServico
    {
        /// <summary>
        /// Busca o grupo do modelo de anamnese pelo ID
        /// </summary>
        /// <param name="modeloAnamneseSgID"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSg>? BuscarPorID(int modeloAnamneseSgID);

        /// <summary>
        /// Busca os grupos dos modelos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<ModeloAnamneseSg>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<ModeloAnamneseSg>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um grupo do modelo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="modeloAnamneseSg"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int modeloAnamneseSgId)> CriarOuAtualizar(CriarModeloAnamneseSgInputModel modeloAnamneseSg, bool atualizaSeExistir);
    }
}
