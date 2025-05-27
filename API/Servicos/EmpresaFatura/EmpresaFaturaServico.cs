using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.EmpresaFaturas;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.EmpresaFaturas
{
    public class EmpresaFaturaServico : ServicoBase, IEmpresaFaturaServico
    {
        private IConfiguration _configuration;
        private IEmpresaFaturaRepo _empresaFaturaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EmpresaFaturaServico(
            IConfiguration configuration,
            IEmpresaFaturaRepo empresaFaturaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaFaturaRepo = empresaFaturaRepo;
        }

        public async Task<EmpresaFatura>? BuscarPorID(int empresaFaturaID) => await _empresaFaturaRepo.BuscarPorID(empresaFaturaID);

        public async Task<List<EmpresaFatura>> BuscarTodos()
        {
            return await _empresaFaturaRepo.BuscarFiltros();
        }

        public async Task Salvar(EmpresaFatura empresaFatura)
        {
            await _empresaFaturaRepo.Adicionar(empresaFatura);
            await Comitar();
        }

        private async Task Atualizar(EmpresaFatura empresaFatura)
        {
            await _empresaFaturaRepo.Atualizar(empresaFatura);
            await Comitar();
        }

        public async Task<(bool criado, int empresaFaturaId)> CriarOuAtualizar(CriarEmpresaFaturaInputModel empresaFatura, bool atualizaSeExistir)
        {
            var cEmpresaFatura = (await _empresaFaturaRepo.Buscar(
                x => x.EmpresaFaturaId == empresaFatura.EmpresaFaturaId
            )).FirstOrDefault();

            if (cEmpresaFatura == null)
            {
                cEmpresaFatura = EmpresaFatura.CriarParaImportacao(
                    empresaAssinaturaID: empresaFatura.EmpresaAssinaturaId,
                    empresaID: empresaFatura.EmpresaId,
                    planoID: empresaFatura.PlanoId,
                    descricao: empresaFatura.Descricao,
                    dataInicio: empresaFatura.DataInicio,
                    dataExpiracao: empresaFatura.DataExpiracao,
                    valor: empresaFatura.Valor,
                    formaPagamento: empresaFatura.FormaPagamento,
                    anexo: empresaFatura.Anexo,
                    situacao: empresaFatura.Situacao,
                    dataPagamento: empresaFatura.DataPagamento,
                    ativo: empresaFatura.Ativo
                );
                await Salvar(cEmpresaFatura);
                return (true, cEmpresaFatura.EmpresaFaturaId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cEmpresaFatura.AtualizarPropriedades(
                    empresaAssinaturaID: empresaFatura.EmpresaAssinaturaId,
                    empresaID: empresaFatura.EmpresaId,
                    planoID: empresaFatura.PlanoId,
                    descricao: empresaFatura.Descricao,
                    dataInicio: empresaFatura.DataInicio,
                    dataExpiracao: empresaFatura.DataExpiracao,
                    valor: empresaFatura.Valor,
                    formaPagamento: empresaFatura.FormaPagamento,
                    anexo: empresaFatura.Anexo,
                    situacao: empresaFatura.Situacao,
                    dataPagamento: empresaFatura.DataPagamento,
                    ativo: empresaFatura.Ativo
                );
                await _empresaFaturaRepo.Atualizar(cEmpresaFatura);
                await Atualizar(cEmpresaFatura);
            }

            return (false, empresaFatura.EmpresaFaturaId);
        }


        public async Task CriarParaImportacao(int empresaFaturaID, Guid empresaAssinaturaID, Guid empresaID, Guid planoID, string descricao, DateTime dataInicio, DateTime dataExpiracao, decimal valor,
                                                        short? formaPagamento, byte[]? anexo, short situacao, DateTime? dataPagamento, bool? ativo)
        {
            var cEmpresaFatura = (await _empresaFaturaRepo.Buscar(
                            x => x.EmpresaFaturaId == empresaFaturaID)
                            ).FirstOrDefault();
            if (cEmpresaFatura == null)
            {
                cEmpresaFatura = EmpresaFatura.CriarParaImportacao(empresaAssinaturaID, empresaID, planoID, descricao, dataInicio, dataExpiracao, valor,
                                                                   formaPagamento, anexo, situacao, dataPagamento, ativo);
                await Salvar(cEmpresaFatura);
            }
            return;
        }

        public async Task Validar(int empresaFaturaID)
        {
            var cEmpresaFatura = (await _empresaFaturaRepo.Buscar(x => x.EmpresaFaturaId == empresaFaturaID)).FirstOrDefault();
            if (cEmpresaFatura == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Fatura da empresa com ID {empresaFaturaID} não encontrado."
                );
            }
        }
    }
}
