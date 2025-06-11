using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Escolaridades
{
    public interface IEscolaridadeServico
    {
        /// <summary>
        /// Busca o tipo de escolaridade pelo ID
        /// </summary>
        /// <param name="escolaridadeID"></param>
        /// <returns></returns>
        Task<Escolaridade>? BuscarPorID(int escolaridadeID);

        /// <summary>
        /// Busca os tipos de escolaridades pelo nome ou parte dele
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        Task<List<Escolaridade>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Buscar todos os tipos de escolaridades
        /// </summary>
        /// <returns></returns>
        Task<List<Escolaridade>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova escolaridade
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<Escolaridade> Adicionar(Escolaridade escolaridade);

        /// <summary>
        /// Atualizar a escolaridade
        /// </summary>
        /// <param name="escolaridade"></param>
        /// <returns></returns>
        Task<Escolaridade> Atualizar(Escolaridade escolaridade);

        /// <summary>
        /// Remover a escolaridade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);
    }
}
