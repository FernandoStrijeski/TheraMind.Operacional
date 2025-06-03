using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseRespostaClientes;
using Dominio.AnamneseSubGrupoQuestoes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Org.BouncyCastle.Ocsp;
using System.Net;

namespace API.Servicos.AnamneseRespostaClientes
{
    public class AnamneseRespostaClienteServico : ServicoBase, IAnamneseRespostaClienteServico
    {
        private IConfiguration _configuration;
        private IAnamneseRespostaClienteRepo _anamneseRespostaClienteRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseRespostaClienteServico(
            IConfiguration configuration,
            IAnamneseRespostaClienteRepo anamneseRespostaClienteRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseRespostaClienteRepo = anamneseRespostaClienteRepo;
        }

        public async Task<AnamneseRespostaCliente>? BuscarPorID(int anamneseSubGrupoQuestaoID) => await _anamneseRespostaClienteRepo.BuscarPorID(anamneseSubGrupoQuestaoID);

        public async Task<List<AnamneseRespostaCliente>> BuscarTodos()
        {
            return await _anamneseRespostaClienteRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseRespostaCliente>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseRespostaClienteRepo.BuscarFiltros(x => x.Resposta.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(AnamneseRespostaCliente anamneseRespostaCliente)
        {
            await _anamneseRespostaClienteRepo.Adicionar(anamneseRespostaCliente);
            await Comitar();
        }

        private async Task Atualizar(AnamneseRespostaCliente anamneseRespostaCliente)
        {
            await _anamneseRespostaClienteRepo.Atualizar(anamneseRespostaCliente);
            await Comitar();
        }

        public async Task<(bool criado, int anamneseSubGrupoQuestaoId)> CriarOuAtualizar(CriarAnamneseRespostaClienteInputModel anamneseRespostaCliente, bool atualizaSeExistir)
        {
            var cAnamneseRespostaCliente = (await _anamneseRespostaClienteRepo.Buscar(
                x => x.AnamneseSubGrupoQuestaoId == anamneseRespostaCliente.AnamneseSubGrupoQuestaoId
            )).FirstOrDefault();

            if (cAnamneseRespostaCliente == null)
            {
                cAnamneseRespostaCliente = AnamneseRespostaCliente.CriarParaImportacao(
                    empresaID: anamneseRespostaCliente.EmpresaId,
                    filialID: anamneseRespostaCliente.FilialId,
                    profissionalID: anamneseRespostaCliente.ProfissionalId,
                    anamneseGrupoID: anamneseRespostaCliente.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseRespostaCliente.AnamneseSubGrupoId,
                    anamneseSubGrupoQuestaoID: anamneseRespostaCliente.AnamneseSubGrupoQuestaoId,
                    clienteID: anamneseRespostaCliente.ClienteID,
                    resposta: anamneseRespostaCliente.Resposta
                );
                await Salvar(cAnamneseRespostaCliente);
                return (true, cAnamneseRespostaCliente.AnamneseSubGrupoQuestaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAnamneseRespostaCliente.AtualizarPropriedades(
                    empresaID: anamneseRespostaCliente.EmpresaId,
                    filialID: anamneseRespostaCliente.FilialId,
                    profissionalID: anamneseRespostaCliente.ProfissionalId,
                    anamneseGrupoID: anamneseRespostaCliente.AnamneseGrupoId,
                    anamneseSubGrupoID: anamneseRespostaCliente.AnamneseSubGrupoId,
                    anamneseSubGrupoQuestaoID: anamneseRespostaCliente.AnamneseSubGrupoQuestaoId,
                    clienteID: anamneseRespostaCliente.ClienteID,
                    resposta: anamneseRespostaCliente.Resposta                    
                );
                await _anamneseRespostaClienteRepo.Atualizar(cAnamneseRespostaCliente);
                await Atualizar(cAnamneseRespostaCliente);
            }

            return (false, anamneseRespostaCliente.AnamneseSubGrupoQuestaoId);
        }


        public async Task CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, int anamneseSubGrupoQuestaoID, Guid clienteID, string? resposta)
        {
            var cAnamneseSubGrupoQuestao = (await _anamneseRespostaClienteRepo.Buscar(
                            x => x.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID)
                            ).FirstOrDefault();
            if (cAnamneseSubGrupoQuestao == null)
            {
                cAnamneseSubGrupoQuestao = AnamneseRespostaCliente.CriarParaImportacao(empresaID, filialID, profissionalID, anamneseGrupoID, anamneseSubGrupoID, anamneseSubGrupoQuestaoID, clienteID, resposta);
                await Salvar(cAnamneseSubGrupoQuestao);
            }
            return;
        }

        public async Task Validar(int anamneseSubGrupoQuestaoID)
        {
            var cAnamneseSubGrupoQuestao = (await _anamneseRespostaClienteRepo.Buscar(x => x.AnamneseSubGrupoQuestaoId == anamneseSubGrupoQuestaoID)).FirstOrDefault();
            if (cAnamneseSubGrupoQuestao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Resposta da questão do subgrupo de anamnese com ID {anamneseSubGrupoQuestaoID} não encontrada."
                );
            }
        }
    }
}
