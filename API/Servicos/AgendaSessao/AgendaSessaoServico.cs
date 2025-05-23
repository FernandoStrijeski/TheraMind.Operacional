using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AgendasSessoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AgendasSessoes
{
    public class AgendaSessaoServico : ServicoBase, IAgendaSessaoServico
    {
        private IConfiguration _configuration;
        private IAgendaSessaoRepo _agendaSessaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaSessaoServico(
            IConfiguration configuration,
            IAgendaSessaoRepo agendaSessaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaSessaoRepo = agendaSessaoRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<AgendaSessao>? BuscarPorID(Guid agendaSessaoID) => await _agendaSessaoRepo.BuscarPorID(agendaSessaoID);

        public async Task<List<AgendaSessao>> BuscarTodos()
        {
            return await _agendaSessaoRepo.BuscarFiltros();
        }

        public async Task<List<AgendaSessao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _agendaSessaoRepo.BuscarFiltros(x => x.Cliente.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(AgendaSessao agendaSessao)
        {
            await _agendaSessaoRepo.Adicionar(agendaSessao);
            await Comitar();
        }

        private async Task Atualizar(AgendaSessao agendaSessao)
        {
            await _agendaSessaoRepo.Atualizar(agendaSessao);
            await Comitar();
        }

        public async Task<(bool criado, Guid agendaSessaoId)> CriarOuAtualizar(CriarAgendaSessaoInputModel agendaSessao, bool atualizaSeExistir)
        {
            var cAgendaSessao = (await _agendaSessaoRepo.Buscar(
                x => x.AgendaSessaoId == agendaSessao.AgendaSessaoId
            )).FirstOrDefault();

            if (cAgendaSessao == null)
            {
                cAgendaSessao = AgendaSessao.CriarParaImportacao(
                    empresaId: agendaSessao.EmpresaId,
                    filialId: agendaSessao.FilialId,
                    profissionalId: agendaSessao.ProfissionalId,
                    agendaProfissionalId: agendaSessao.AgendaProfissionalId,
                    servicoId: agendaSessao.ServicoId,
                    formularioSessaoId: agendaSessao.FormularioSessaoId,
                    clienteId: agendaSessao.ClienteId,
                    tipoEvento: agendaSessao.TipoEvento,
                    modalidade: agendaSessao.Modalidade,
                    salaId: agendaSessao.SalaId,
                    dataHoraInicio: agendaSessao.DataHoraInicio,
                    dataHoraFim: agendaSessao.DataHoraFim,
                    diaTodo: agendaSessao.DiaTodo,
                    tipoRecorrencia: agendaSessao.TipoRecorrencia,
                    recorrenciaDataTermino: agendaSessao.RecorrenciaDataTermino,
                    recorrenciaNroSessoes: agendaSessao.RecorrenciaNroSessoes,
                    situacao: agendaSessao.Situacao,
                    pagamentoEfetuado: agendaSessao.PagamentoEfetuado,
                    dataCancelamento: agendaSessao.DataCancelamento,
                    motivoCancelamento: agendaSessao.MotivoCancelamento,
                    mantemCobranca: agendaSessao.MantemCobranca
                );
                await Salvar(cAgendaSessao);
                return (true, cAgendaSessao.AgendaSessaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAgendaSessao.AtualizarPropriedades(
                    empresaId: agendaSessao.EmpresaId,
                    filialId: agendaSessao.FilialId,
                    profissionalId: agendaSessao.ProfissionalId,
                    agendaProfissionalId: agendaSessao.AgendaProfissionalId,
                    servicoId: agendaSessao.ServicoId,
                    formularioSessaoId: agendaSessao.FormularioSessaoId,
                    clienteId: agendaSessao.ClienteId,
                    tipoEvento: agendaSessao.TipoEvento,
                    modalidade: agendaSessao.Modalidade,
                    salaId: agendaSessao.SalaId,
                    dataHoraInicio: agendaSessao.DataHoraInicio,
                    dataHoraFim: agendaSessao.DataHoraFim,
                    diaTodo: agendaSessao.DiaTodo,
                    tipoRecorrencia: agendaSessao.TipoRecorrencia,
                    recorrenciaDataTermino: agendaSessao.RecorrenciaDataTermino,
                    recorrenciaNroSessoes: agendaSessao.RecorrenciaNroSessoes,
                    situacao: agendaSessao.Situacao,
                    pagamentoEfetuado: agendaSessao.PagamentoEfetuado,
                    dataCancelamento: agendaSessao.DataCancelamento,
                    motivoCancelamento: agendaSessao.MotivoCancelamento,
                    mantemCobranca: agendaSessao.MantemCobranca
                );
                await _agendaSessaoRepo.Atualizar(cAgendaSessao);
                await Atualizar(cAgendaSessao);
            }

            return (false, agendaSessao.AgendaSessaoId);
        }


        public async Task CriarParaImportacao(Guid agendaSessaoID, Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId,
                                             Guid? clienteId, short tipoEvento, short modalidade, string? salaId, DateTime dataHoraInicio, DateTime dataHoraFim,
                                             bool? diaTodo, short tipoRecorrencia, DateTime? recorrenciaDataTermino, short? recorrenciaNroSessoes, short situacao,
                                             bool? pagamentoEfetuado, DateTime? dataCancelamento, string? motivoCancelamento, bool? mantemCobranca)
        {
            var cAgendaSessao = (await _agendaSessaoRepo.Buscar(
                            x => x.AgendaSessaoId == agendaSessaoID)
                            ).FirstOrDefault();
            if (cAgendaSessao == null)
            {
                cAgendaSessao = AgendaSessao.CriarParaImportacao(empresaId, filialId, profissionalId, agendaProfissionalId, servicoId, formularioSessaoId,
                                                        clienteId, tipoEvento, modalidade, salaId, dataHoraInicio, dataHoraFim,
                                                        diaTodo, tipoRecorrencia, recorrenciaDataTermino, recorrenciaNroSessoes, situacao,
                                                        pagamentoEfetuado, dataCancelamento, motivoCancelamento, mantemCobranca);
                await Salvar(cAgendaSessao);
            }
            return;
        }

        public async Task Validar(Guid agendaSessaoID)
        {
            var cAgendaSessao = (await _agendaSessaoRepo.Buscar(x => x.AgendaSessaoId == agendaSessaoID)).FirstOrDefault();
            if (cAgendaSessao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Sessão na agenda com ID {agendaSessaoID} não encontrado."
                );
            }
        }
    }
}
