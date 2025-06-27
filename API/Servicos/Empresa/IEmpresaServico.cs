using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Empresas
{
    public interface IEmpresaServico
    {
        /// <summary>
        /// Busca a empresa pelo ID
        /// </summary>
        /// <param name="empresaID"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Empresa>? BuscarPorID(Guid empresaID);

        /// <summary>
        /// Adicionar uma nova empresa
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Empresa> Adicionar(Dominio.Entidades.Empresa empresa);

        /// <summary>
        /// Atualizar a empresa
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Empresa> Atualizar(Dominio.Entidades.Empresa empresa);

        /// <summary>
        /// Buscar todas as empresas
        /// </summary>
        /// <returns></returns>
        Task<List<Dominio.Entidades.Empresa>> BuscarTodos();

        /// <summary>
        /// Remover a empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);
    }
}
