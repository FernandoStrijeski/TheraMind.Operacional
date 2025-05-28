using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.ModelosAnamneseSGQuestoes
{
    public interface IModeloAnamneseSgQuestaoServico
    {
        /// <summary>
        /// Busca a questão do grupo do modelo de anamnese pelo ID
        /// </summary>
        /// <param name="modeloAnamneseSgQuestaoID"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestao>? BuscarPorID(int modeloAnamneseSgQuestaoID);

        /// <summary>
        /// Busca as questões dos grupos dos modelos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<ModeloAnamneseSgQuestao>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as questões dos grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<ModeloAnamneseSgQuestao>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza a questão de um grupo do modelo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="modeloAnamneseSgQuestao"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int modeloAnamneseSgId)> CriarOuAtualizar(CriarModeloAnamneseSgQuestaoInputModel modeloAnamneseSgQuestao, bool atualizaSeExistir);
    }
}
