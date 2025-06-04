using API.Core.Exceptions;
using API.modelos.InputModels;
using Dominio.AcompanhamentosClinicos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AcompanhamentosClinicos
{
    public class AcompanhamentoClinicoServico : ServicoBase, IAcompanhamentoClinicoServico
    {
        private IConfiguration _configuration;
        private IAcompanhamentoClinicoRepo _acompanhamentoClinicoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AcompanhamentoClinicoServico(
            IConfiguration configuration,
            IAcompanhamentoClinicoRepo acompanhamentoClinicoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _acompanhamentoClinicoRepo = acompanhamentoClinicoRepo;
        }

        public async Task<AcompanhamentoClinico>? BuscarPorID(Guid acompanhamentoClinicoID) => await _acompanhamentoClinicoRepo.BuscarPorID(acompanhamentoClinicoID);

        public async Task<List<AcompanhamentoClinico>> BuscarTodos() => await _acompanhamentoClinicoRepo.BuscarFiltros();       

        public async Task<List<AcompanhamentoClinico>> BuscarTodosPorProfissionalCliente(Guid profissionalID, Guid clienteID) {
            return await _acompanhamentoClinicoRepo.BuscarFiltros(x => x.ProfissionalId == profissionalID && x.ClienteId == clienteID);
        }

        public async Task Salvar(AcompanhamentoClinico acompanhamentoClinico)
        {
            await _acompanhamentoClinicoRepo.Adicionar(acompanhamentoClinico);
            await Comitar();
        }

        private async Task Atualizar(AcompanhamentoClinico acompanhamentoClinico)
        {
            await _acompanhamentoClinicoRepo.Atualizar(acompanhamentoClinico);
            await Comitar();
        }

        public async Task<(bool criado, Guid acompanhamentoClinicoId)> CriarOuAtualizar(CriarAcompanhamentoClinicoInputModel acompanhamentoClinico, bool atualizaSeExistir)
        {
            var cAcompanhamentoClinico = (await _acompanhamentoClinicoRepo.Buscar(
                x => x.AcompanhamentoClinicoId == acompanhamentoClinico.AcompanhamentoClinicoId
            )).FirstOrDefault();

            if (cAcompanhamentoClinico == null)
            {
                cAcompanhamentoClinico = AcompanhamentoClinico.CriarParaImportacao(
                    empresaID: acompanhamentoClinico.EmpresaId,
                    filialID: acompanhamentoClinico.FilialId,
                    profissionalID: acompanhamentoClinico.ProfissionalId,
                    clienteID: acompanhamentoClinico.ClienteId,
                    avaliacaoDemanda: acompanhamentoClinico.AvaliacaoDemanda,
                    planoTratamento: acompanhamentoClinico.PlanoTratamento,
                    diagnostico: acompanhamentoClinico.Diagnostico,
                    registroEncerramento: acompanhamentoClinico.RegistroEncerramento
                );
                await Salvar(cAcompanhamentoClinico);
                return (true, cAcompanhamentoClinico.AcompanhamentoClinicoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAcompanhamentoClinico.AtualizarPropriedades(
                    empresaID: acompanhamentoClinico.EmpresaId,
                    filialID: acompanhamentoClinico.FilialId,
                    profissionalID: acompanhamentoClinico.ProfissionalId,
                    clienteID: acompanhamentoClinico.ClienteId,
                    avaliacaoDemanda: acompanhamentoClinico.AvaliacaoDemanda,
                    planoTratamento: acompanhamentoClinico.PlanoTratamento,
                    diagnostico: acompanhamentoClinico.Diagnostico,
                    registroEncerramento: acompanhamentoClinico.RegistroEncerramento
                );
                await _acompanhamentoClinicoRepo.Atualizar(cAcompanhamentoClinico);
                await Atualizar(cAcompanhamentoClinico);
            }

            return (false, acompanhamentoClinico.AcompanhamentoClinicoId);
        }


        public async Task CriarParaImportacao(Guid acompanhamentoClinicoID, Guid empresaID, int filialID, Guid profissionalID, Guid? clienteID, string? avaliacaoDemanda, string? planoTratamento, string? diagnostico, string? registroEncerramento)
        {
            var cAcompanhamentoClinico = (await _acompanhamentoClinicoRepo.Buscar(
                            x => x.AcompanhamentoClinicoId == acompanhamentoClinicoID)
                            ).FirstOrDefault();
            if (cAcompanhamentoClinico == null)
            {
                cAcompanhamentoClinico = AcompanhamentoClinico.CriarParaImportacao(empresaID, filialID, profissionalID, clienteID, avaliacaoDemanda, planoTratamento, diagnostico, registroEncerramento);
                await Salvar(cAcompanhamentoClinico);
            }
            return;
        }

        public async Task Validar(Guid acompanhamentoClinicoID)
        {
            var cAcompanhamentoClinico = (await _acompanhamentoClinicoRepo.Buscar(x => x.AcompanhamentoClinicoId == acompanhamentoClinicoID)).FirstOrDefault();
            if (cAcompanhamentoClinico == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Acompanhamento clinico com ID {acompanhamentoClinicoID} não encontrado."
                );
            }
        }
    }
}
