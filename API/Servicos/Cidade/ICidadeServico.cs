using API.modelos;
using Dominio.Entidades;

namespace API.Servicos.Cidades
{
    public interface ICidadeServico
    {
        /// <summary>
        /// Busca a cidade pelo ID
        /// </summary>
        /// <param name="cidadeID"></param>
        /// <returns></returns>
        Task<Cidade>? BuscarPorID(int cidadeID);

        /// <summary>
        /// Busca as cidades pelo nome ou parte dele
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        Task<List<Cidade>> BuscarPorNome(BuscarComNomeParametro parametro);

        /// <summary>
        /// Busca a cidade pelo código IBGE
        /// </summary>
        /// <param name="codigoIBGE"></param>
        /// <returns></returns>
        Task<Cidade>? BuscarPorIBGE(int codigoIBGE);

        /// <summary>
        /// Buscar todas as cidades
        /// </summary>
        /// <returns></returns>
        Task<List<Cidade>> BuscarTodos();

        /// <summary>
        /// Adicionar uma nova cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        Task<Cidade> Adicionar(Cidade cidade);

        /// <summary>
        /// Atualizar a cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        Task<Cidade> Atualizar(Cidade cidade);

        /// <summary>
        /// Remover a cidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
