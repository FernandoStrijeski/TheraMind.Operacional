using API.modelos.InputModels;

namespace API.Servicos.Filiais
{
    public interface IFilialServico
    {
        /// <summary>
        /// Busca a Filial pelo ID da empresa e seu ID
        /// </summary>
        /// <param name="empresaID"></param>
        /// <param name="filialID"></param>
        /// <returns></returns>
        Task<Dominio.Entidades.Filial>? BuscarPorID(Guid empresaID, int filialID);

        /// <summary>
        /// Cria ou atualiza uma filial a partir do modelo informado
        /// </summary>
        /// <param name="filial"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, int filialId)> CriarOuAtualizar(CriarFilialInputModel filial, bool atualizaSeExistir);
    }
}
