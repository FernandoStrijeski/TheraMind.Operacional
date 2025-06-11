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
        /// Adicionar uma nova questão do subgrupo dos modelos de anamnese
        /// </summary>
        /// <param name="modeloAnamneseSgQuestao"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestao> Adicionar(ModeloAnamneseSgQuestao modeloAnamneseSgQuestao);

        /// <summary>
        /// Atualizar a questão do subgrupo dos modelos de anamnese
        /// </summary>
        /// <param name="modeloAnamneseSgQuestao"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestao> Atualizar(ModeloAnamneseSgQuestao modeloAnamneseSgQuestao);

        /// <summary>
        /// Remover a questão do subgrupo dos modelos de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
