using API.modelos.InputModels;

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
        /// Cria ou atualiza uma empresa a partir do modelo informado
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid empresaId)> CriarOuAtualizar(CriarEmpresaInputModel empresa, bool atualizaSeExistir);
    }
}
