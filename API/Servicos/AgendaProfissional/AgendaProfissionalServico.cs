using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AgendasProfissionais;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AgendasProfissionais
{
    public class AgendaProfissionalServico : ServicoBase, IAgendaProfissionalServico
    {
        private IConfiguration _configuration;
        private IAgendaProfissionalRepo _agendaProfissionalRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaProfissionalServico(
            IConfiguration configuration,
            IAgendaProfissionalRepo agendaProfissionalRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaProfissionalRepo = agendaProfissionalRepo;
        }

        public async Task<AgendaProfissional>? BuscarPorID(int agendaProfissionalID) => await _agendaProfissionalRepo.BuscarPorID(agendaProfissionalID);

        public async Task<List<AgendaProfissional>> BuscarTodos()
        {
            return await _agendaProfissionalRepo.BuscarFiltros();
        }

        public async Task Salvar(AgendaProfissional agendaProfissional)
        {
            await _agendaProfissionalRepo.Adicionar(agendaProfissional);
            await Comitar();
        }

        private async Task Atualizar(AgendaProfissional agendaProfissional)
        {
            await _agendaProfissionalRepo.Atualizar(agendaProfissional);
            await Comitar();
        }

        public async Task<(bool criado, int agendaProfissionalId)> CriarOuAtualizar(CriarAgendaProfissionalInputModel agendaProfissional, bool atualizaSeExistir)
        {
            var cAgendaProfissional = (await _agendaProfissionalRepo.Buscar(
                x => x.AgendaProfissionalId == agendaProfissional.AgendaProfissionalId
            )).FirstOrDefault();

            if (cAgendaProfissional == null)
            {
                cAgendaProfissional = AgendaProfissional.CriarParaImportacao(
                empresaID: agendaProfissional.EmpresaId,
                filialID: agendaProfissional.FilialId,
                profissionalID: agendaProfissional.ProfissionalId,
                exibicaoEmMinutos: agendaProfissional.ExibicaoEmMinutos,
                duracaoSessaoMinutos: agendaProfissional.DuracaoSessaoMinutos,
                horaInicio: agendaProfissional.HoraInicio,
                horaFim: agendaProfissional.HoraFim,
                diasOcultados: agendaProfissional.DiasOcultados,
                exibeSessoesAusentesCanc: agendaProfissional.ExibeSessoesAusentesCanc,
                exibeComparecimento: agendaProfissional.ExibeComparecimento,
                exibePagamento: agendaProfissional.ExibePagamento,
                exibeFeriadosNacionais: agendaProfissional.ExibeFeriadosNacionais,
                tipoVisualizacao: agendaProfissional.TipoVisualizacao
                );
                await Salvar(cAgendaProfissional);
                return (true, cAgendaProfissional.AgendaProfissionalId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAgendaProfissional.AtualizarPropriedades(
                    empresaID: agendaProfissional.EmpresaId,
                    filialID: agendaProfissional.FilialId,
                    profissionalID: agendaProfissional.ProfissionalId,
                    exibicaoEmMinutos: agendaProfissional.ExibicaoEmMinutos,
                    duracaoSessaoMinutos: agendaProfissional.DuracaoSessaoMinutos,
                    horaInicio: agendaProfissional.HoraInicio,
                    horaFim: agendaProfissional.HoraFim,
                    diasOcultados: agendaProfissional.DiasOcultados,
                    exibeSessoesAusentesCanc: agendaProfissional.ExibeSessoesAusentesCanc,
                    exibeComparecimento: agendaProfissional.ExibeComparecimento,
                    exibePagamento: agendaProfissional.ExibePagamento,
                    exibeFeriadosNacionais: agendaProfissional.ExibeFeriadosNacionais,
                    tipoVisualizacao: agendaProfissional.TipoVisualizacao
                );
                await _agendaProfissionalRepo.Atualizar(cAgendaProfissional);
                await Atualizar(cAgendaProfissional);
            }

            return (false, agendaProfissional.AgendaProfissionalId);
        }


        public async Task CriarParaImportacao(int agendaProfissionalId, Guid empresaID, int filialID, Guid profissionalID, int exibicaoEmMinutos, int duracaoSessaoMinutos, TimeSpan horaInicio, TimeSpan horaFim,
                                              string? diasOcultados, bool? exibeSessoesAusentesCanc, bool? exibeComparecimento, bool? exibePagamento, bool? exibeFeriadosNacionais, short tipoVisualizacao)
        {
            var cAgendaProfissional = (await _agendaProfissionalRepo.Buscar(
                            x => x.AgendaProfissionalId == agendaProfissionalId)
                            ).FirstOrDefault();
            if (cAgendaProfissional == null)
            {
                cAgendaProfissional = AgendaProfissional.CriarParaImportacao(empresaID, filialID, profissionalID, exibicaoEmMinutos, duracaoSessaoMinutos, horaInicio, horaFim,
                                                            diasOcultados, exibeSessoesAusentesCanc, exibeComparecimento, exibePagamento, exibeFeriadosNacionais, tipoVisualizacao);
                await Salvar(cAgendaProfissional);
            }
            return;
        }

        public async Task Validar(int agendaProfissionalID)
        {
            var cAgendaProfissional = (await _agendaProfissionalRepo.Buscar(x => x.AgendaProfissionalId == agendaProfissionalID)).FirstOrDefault();
            if (cAgendaProfissional == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Agenda profissional com ID {agendaProfissionalID} não encontrado."
                );
            }
        }
    }
}
