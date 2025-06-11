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
        /// Adicionar uma opção da questão do subgrupo do modelo de anamnese
        /// </summary>
        /// <param name="modeloAnamneseSgQuestaoO"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestaoO> Adicionar(ModeloAnamneseSgQuestaoO modeloAnamneseSgQuestaoO);

        /// <summary>
        /// Atualizar a opção da questão do subgrupo do modelo de anamnese
        /// </summary>
        /// <param name="modeloAnamneseSgQuestaoO"></param>
        /// <returns></returns>
        Task<ModeloAnamneseSgQuestaoO> Atualizar(ModeloAnamneseSgQuestaoO modeloAnamneseSgQuestaoO);

        /// <summary>
        /// Remover a opção da questão do subgrupo do modelo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
