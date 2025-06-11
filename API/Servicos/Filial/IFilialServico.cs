using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Filiais
{
    public interface IFilialServico
    {
        /// <summary>
        /// Busca a Filial pelo ID
        /// </summary>
        /// <param name="filialID"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Filial>? BuscarPorID(int filialID);

        /// <summary>
        /// Busca as filiais pelo nome ou parte dele
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        Task<List<Dominio.Entidades.Filial>> BuscarPorNome(BuscarComNomeParametro parametros);

        /// <summary>
        /// Busca todas as filiais da empresa do parametro
        /// </summary>
        /// <param name="empresaID"></param>
        /// <returns></returns>
        Task<List<Dominio.Entidades.Filial>>? BuscarTodasPorEmpresa(Guid empresaID);

        /// <summary>
        /// Adicionar uma nova filial
        /// </summary>
        /// <param name="filial"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Filial> Adicionar(Dominio.Entidades.Filial filial);

        /// <summary>
        /// Atualizar a filial
        /// </summary>
        /// <param name="filial"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Filial> Atualizar(Dominio.Entidades.Filial filial);

        /// <summary>
        /// Remover a filial
        /// </summary>
        /// <param name="empresaId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(int id);

    }
}
