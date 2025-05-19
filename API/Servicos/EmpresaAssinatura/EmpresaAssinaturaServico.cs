using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.EmpresasAssinaturas;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.EmpresasAssinaturas
{
    public class EmpresaAssinaturaServico : ServicoBase, IEmpresaAssinaturaServico
    {
        private IConfiguration _configuration;
        private IEmpresaAssinaturaRepo _empresaAssinaturaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EmpresaAssinaturaServico(
            IConfiguration configuration,
            IEmpresaAssinaturaRepo empresaAssinaturaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _empresaAssinaturaRepo = empresaAssinaturaRepo;
        }

        public async Task<EmpresaAssinatura>? BuscarPorID(Guid empresaAssinaturaID) => await _empresaAssinaturaRepo.BuscarPorID(empresaAssinaturaID);

        public async Task<List<EmpresaAssinatura>> BuscarTodos()
        {
            return await _empresaAssinaturaRepo.BuscarFiltros();
        }

        public async Task<List<EmpresaAssinatura>> BuscarPorIdEmpresa(Guid empresaID) => await _empresaAssinaturaRepo.BuscarPorIdEmpresa(empresaID);


        public async Task Salvar(EmpresaAssinatura empresaAssinatura)
        {
            await _empresaAssinaturaRepo.Adicionar(empresaAssinatura);
            await Comitar();
        }

        private async Task Atualizar(EmpresaAssinatura empresaAssinatura)
        {
            await _empresaAssinaturaRepo.Atualizar(empresaAssinatura);
            await Comitar();
        }

        public async Task<(bool criado, Guid empresaAssinaturaId)> CriarOuAtualizar(CriarEmpresaAssinaturaInputModel empresaAssinatura, bool atualizaSeExistir)
        {
            var cEmpresaAssinatura = (await _empresaAssinaturaRepo.Buscar(
                x => x.EmpresaAssinaturaId == empresaAssinatura.EmpresaAssinaturaId
            )).FirstOrDefault();

            if (cEmpresaAssinatura == null)
            {
                cEmpresaAssinatura = EmpresaAssinatura.CriarParaImportacao(
                    empresaID: empresaAssinatura.EmpresaId,
                    planoID: empresaAssinatura.PlanoId,
                    tipoPlano: empresaAssinatura.TipoPlano,
                    valorAtual: empresaAssinatura.ValorAtual,
                    descontoPromocional: empresaAssinatura.DescontoPromocional,
                    descontoMeses: empresaAssinatura.DescontoMeses,
                    dataExpiracao: empresaAssinatura.DataExpiracao,                
                    ativo: empresaAssinatura.Ativo
                );
                await Salvar(cEmpresaAssinatura);
                return (true, cEmpresaAssinatura.EmpresaAssinaturaId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cEmpresaAssinatura.AtualizarPropriedades(
                    empresaID: empresaAssinatura.EmpresaId,
                    planoID: empresaAssinatura.PlanoId,
                    tipoPlano: empresaAssinatura.TipoPlano,
                    valorAtual: empresaAssinatura.ValorAtual,
                    descontoPromocional: empresaAssinatura.DescontoPromocional,
                    descontoMeses: empresaAssinatura.DescontoMeses,
                    dataExpiracao: empresaAssinatura.DataExpiracao,
                    ativo: empresaAssinatura.Ativo
                );
                await _empresaAssinaturaRepo.Atualizar(cEmpresaAssinatura);
                await Atualizar(cEmpresaAssinatura);
            }

            return (false, empresaAssinatura.EmpresaAssinaturaId);
        }


        public async Task CriarParaImportacao(Guid empresaAssinaturaID, Guid empresaID, Guid planoID, short tipoPlano, decimal valorAtual, decimal? descontoPromocional, short? descontoMeses, DateTime? dataExpiracao, bool? ativo)
        {
            var cEmpresaAssinatura = (await _empresaAssinaturaRepo.Buscar(
                            x => x.EmpresaAssinaturaId == empresaAssinaturaID)
                            ).FirstOrDefault();
            if (cEmpresaAssinatura == null)
            {
                cEmpresaAssinatura = EmpresaAssinatura.CriarParaImportacao(empresaID, planoID, tipoPlano, valorAtual, descontoPromocional, descontoMeses, dataExpiracao, ativo);
                await Salvar(cEmpresaAssinatura);
            }
            return;
        }

        public async Task Validar(Guid empresaAssinaturaID)
        {
            var cEmpresaAssinatura = (await _empresaAssinaturaRepo.Buscar(x => x.EmpresaAssinaturaId == empresaAssinaturaID)).FirstOrDefault();
            if (cEmpresaAssinatura == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Assinatura de empresa com ID {empresaAssinaturaID} não encontrado."
                );
            }
        }
    }
}
