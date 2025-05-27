using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.AcompanhamentosClinicos
{
    public interface IAcompanhamentoClinicoServico
    {
        /// <summary>
        /// Busca o acompanhamento clínico pelo ID
        /// </summary>
        /// <param name="acompanhamentoClinicoID"></param>
        /// <returns></returns>
        Task<AcompanhamentoClinico>? BuscarPorID(Guid acompanhamentoClinicoID);

        /// <summary>
        /// Buscar todos os acompanhamentos clínicos
        /// </summary>
        /// <returns></returns>
        Task<List<AcompanhamentoClinico>> BuscarTodos();

        /// <summary>
        /// Cria ou atualiza um acompanhamento clínico a partir do modelo informado
        /// </summary>
        /// <param name="acompanhamentoClinico"></param>
        /// <param name="atualizaSeExistir"></param>
        /// <returns></returns>
        Task<(bool criado, Guid acompanhamentoClinicoId)> CriarOuAtualizar(CriarAcompanhamentoClinicoInputModel acompanhamentoClinico, bool atualizaSeExistir);
    }
}
