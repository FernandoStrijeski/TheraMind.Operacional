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
        /// Buscar todos os acompanhamentos clínicos do profissional e cliente
        /// </summary>
        /// <param name="profissionalID"></param>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        Task<List<AcompanhamentoClinico>> BuscarTodosPorProfissionalCliente(Guid profissionalID, Guid clienteID);

        /// <summary>
        /// Adicionar um novo acompanhamento clínico
        /// </summary>
        /// <param name="acompanhamentoClinico"></param>
        /// <returns></returns>
        Task<AcompanhamentoClinico> Adicionar(AcompanhamentoClinico acompanhamentoClinico);

        /// <summary>
        /// Atualizar o acompanhamento clínico
        /// </summary>
        /// <param name="acompanhamentoClinico"></param>
        /// <returns></returns>
        Task<AcompanhamentoClinico> Atualizar(AcompanhamentoClinico acompanhamentoClinico);

        /// <summary>
        /// Remover o acompanhamento clínico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Deletar(Guid id);

    }
}
