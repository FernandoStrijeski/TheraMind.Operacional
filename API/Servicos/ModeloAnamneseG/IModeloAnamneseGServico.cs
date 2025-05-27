using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.ModelosAnamneseG
{
    public interface IModeloAnamneseGServico
    {
        /// <summary>
        /// Busca o modelo de anamnese pelo ID
        /// </summary>
        /// <param name="modeloAnamneseGID"></param>
        /// <returns></returns>
        Task<ModeloAnamneseG>? BuscarPorID(int modeloAnamneseGID);

        /// <summary>
        /// Busca os modelos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<ModeloAnamneseG>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os modelos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<ModeloAnamneseG>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um modelo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="modeloAnamneseG"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int modeloAnamneseGId)> CriarOuAtualizar(CriarModeloAnamneseGInputModel modeloAnamneseG, bool atualizaSeExistir);
    }
}
