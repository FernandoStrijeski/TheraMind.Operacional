using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.ModelosAnamneseSGQuestaoOpcoes
{
    public interface IModeloAnamneseSgQuestaoOServico
    {
        /// <summary>
        /// Busca a opção da questão do grupo do modelo de anamnese pelo ID
        /// </summary>
        /// <param name="modeloAnamneseSgQuestaoOID"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestaoO>? BuscarPorID(int modeloAnamneseSgQuestaoOID);

        /// <summary>
        /// Busca as opções das questões dos grupos dos modelos de anamnese pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<ModeloAnamneseSgQuestaoO>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todas as opções das questões dos grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        Task<List<ModeloAnamneseSgQuestaoO>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza a opção da questão de um grupo do modelo de anamnese a partir do modelo informado
        /// </summary>
        /// <param name="modeloAnamneseSgQuestaoO"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int modeloAnamneseSgQuestaoOId)> CriarOuAtualizar(CriarModeloAnamneseSgQuestaoOInputModel modeloAnamneseSgQuestaoO, bool atualizaSeExistir);
    }
}
