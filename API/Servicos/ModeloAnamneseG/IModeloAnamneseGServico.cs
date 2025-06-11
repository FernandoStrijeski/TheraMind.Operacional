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
        /// Adicionar uma grupo de modelo de anamnese
        /// </summary>
        /// <param name="modeloAnamneseG"></param>
        /// <returns></returns>
        Task<ModeloAnamneseG> Adicionar(ModeloAnamneseG modeloAnamneseG);

        /// <summary>
        /// Atualizar o grupo de modelo de anamnese
        /// </summary>
        /// <param name="modeloAnamneseG"></param>
        /// <returns></returns>
        Task<ModeloAnamneseG> Atualizar(ModeloAnamneseG modeloAnamneseG);

        /// <summary>
        /// Remover o grupo de modelo de anamnese
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
