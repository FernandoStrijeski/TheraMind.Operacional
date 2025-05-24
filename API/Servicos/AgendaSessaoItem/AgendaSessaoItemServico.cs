using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.AgendasSessaoItens;
using Dominio.AgendasSessaoItens;
using Dominio.AgendasSessoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AgendaSessaoItens
{
    public class AgendaSessaoItemServico : ServicoBase, IAgendaSessaoItemServico
    {
        private IConfiguration _configuration;
        private IAgendaSessaoItemRepo _agendaSessaoItemRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AgendaSessaoItemServico(
            IConfiguration configuration,
            IAgendaSessaoItemRepo agendaSessaoItemRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _agendaSessaoItemRepo = agendaSessaoItemRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<AgendaSessaoItem>? BuscarPorID(int agendaSessaoItemID) => await _agendaSessaoItemRepo.BuscarPorID(agendaSessaoItemID);

        public async Task<List<AgendaSessaoItem>> BuscarTodos()
        {
            return await _agendaSessaoItemRepo.BuscarFiltros();
        }

        public async Task<List<AgendaSessaoItem>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _agendaSessaoItemRepo.BuscarFiltros(x => x.Cliente.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(AgendaSessaoItem agendaSessao)
        {
            await _agendaSessaoItemRepo.Adicionar(agendaSessao);
            await Comitar();
        }

        private async Task Atualizar(AgendaSessaoItem agendaSessao)
        {
            await _agendaSessaoItemRepo.Atualizar(agendaSessao);
            await Comitar();
        }

        public async Task<(bool criado, int agendaSessaoItemId)> CriarOuAtualizar(CriarAgendaSessaoItemInputModel agendaSessaoItem, bool atualizaSeExistir)
        {
            var cAgendaSessaoItem = (await _agendaSessaoItemRepo.Buscar(
                x => x.AgendaSessaoItemId == agendaSessaoItem.AgendaSessaoItemId
            )).FirstOrDefault();

            if (cAgendaSessaoItem == null)
            {
                cAgendaSessaoItem = AgendaSessaoItem.CriarParaImportacao(
                    empresaId: agendaSessaoItem.EmpresaId,
                    filialId: agendaSessaoItem.FilialId,
                    profissionalId: agendaSessaoItem.ProfissionalId,
                    agendaProfissionalId: agendaSessaoItem.AgendaProfissionalId,
                    servicoId: agendaSessaoItem.ServicoId,
                    formularioSessaoId: agendaSessaoItem.FormularioSessaoId,
                    formularioSessaoCampoId: agendaSessaoItem.FormularioSessaoCampoId,
                    clienteId: agendaSessaoItem.ClienteId,
                    agendaSessaoId: agendaSessaoItem.AgendaSessaoId,
                    campoTipo: agendaSessaoItem.CampoTipo,
                    conteudoTexto: agendaSessaoItem.ConteudoTexto,
                    conteudoArquivo: agendaSessaoItem.ConteudoArquivo,
                    ativo: agendaSessaoItem.Ativo
                );
                await Salvar(cAgendaSessaoItem);
                return (true, cAgendaSessaoItem.AgendaSessaoItemId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAgendaSessaoItem.AtualizarPropriedades(
                    empresaId: agendaSessaoItem.EmpresaId,
                    filialId: agendaSessaoItem.FilialId,
                    profissionalId: agendaSessaoItem.ProfissionalId,
                    agendaProfissionalId: agendaSessaoItem.AgendaProfissionalId,
                    servicoId: agendaSessaoItem.ServicoId,
                    formularioSessaoId: agendaSessaoItem.FormularioSessaoId,
                    formularioSessaoCampoId: agendaSessaoItem.FormularioSessaoCampoId,
                    clienteId: agendaSessaoItem.ClienteId,
                    agendaSessaoId: agendaSessaoItem.AgendaSessaoId,
                    campoTipo: agendaSessaoItem.CampoTipo,
                    conteudoTexto: agendaSessaoItem.ConteudoTexto,
                    conteudoArquivo: agendaSessaoItem.ConteudoArquivo,
                    ativo: agendaSessaoItem.Ativo
                );
                await _agendaSessaoItemRepo.Atualizar(cAgendaSessaoItem);
                await Atualizar(cAgendaSessaoItem);
            }

            return (false, agendaSessaoItem.AgendaSessaoItemId);
        }


        public async Task CriarParaImportacao(int agendaSessaoItemID, Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId, int formularioSessaoCampoId,
                                                           Guid? clienteId, Guid agendaSessaoId, short campoTipo, string campoNome, string? campoTexto, byte[]? campoArquivo, bool? ativo)
        {
            var cAgendaSessaoItem = (await _agendaSessaoItemRepo.Buscar(
                            x => x.AgendaSessaoItemId == agendaSessaoItemID)
                            ).FirstOrDefault();
            if (cAgendaSessaoItem == null)
            {
                cAgendaSessaoItem = AgendaSessaoItem.CriarParaImportacao(empresaId, filialId, profissionalId, agendaProfissionalId, servicoId, formularioSessaoId, formularioSessaoCampoId,
                                                            clienteId, agendaSessaoId, campoTipo, campoTexto, campoArquivo, ativo);
                await Salvar(cAgendaSessaoItem);
            }
            return;
        }

        public async Task Validar(int agendaSessaoItemID)
        {
            var cAgendaSessaoItem = (await _agendaSessaoItemRepo.Buscar(x => x.AgendaSessaoItemId == agendaSessaoItemID)).FirstOrDefault();
            if (cAgendaSessaoItem == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Item da sessão na agenda com ID {agendaSessaoItemID} não encontrado."
                );
            }
        }
    }
}
