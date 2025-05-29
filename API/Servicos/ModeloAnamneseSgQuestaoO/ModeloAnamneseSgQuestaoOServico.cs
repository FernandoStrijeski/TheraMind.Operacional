using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseSGQuestaoOpcoes;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.ModelosAnamneseSGQuestaoOpcoes
{
    public class ModeloAnamneseSgQuestaoOServico : ServicoBase, IModeloAnamneseSgQuestaoOServico
    {
        private IConfiguration _configuration;
        private IModeloAnamneseSgQuestaoORepo _modeloAnamneseSgQuestaoORepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ModeloAnamneseSgQuestaoOServico(
            IConfiguration configuration,
            IModeloAnamneseSgQuestaoORepo modeloAnamneseSgQuestaoORepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _modeloAnamneseSgQuestaoORepo = modeloAnamneseSgQuestaoORepo;
        }

        public async Task<ModeloAnamneseSgQuestaoO>? BuscarPorID(int modeloAnamneseSgOID) => await _modeloAnamneseSgQuestaoORepo.BuscarPorID(modeloAnamneseSgOID);

        public async Task<List<ModeloAnamneseSgQuestaoO>> BuscarTodos()
        {
            return await _modeloAnamneseSgQuestaoORepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseSgQuestaoO>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseSgQuestaoORepo.BuscarFiltros(x => x.Texto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(ModeloAnamneseSgQuestaoO modeloAnamneseSgO)
        {
            await _modeloAnamneseSgQuestaoORepo.Adicionar(modeloAnamneseSgO);
            await Comitar();
        }

        private async Task Atualizar(ModeloAnamneseSgQuestaoO modeloAnamneseSgO)
        {
            await _modeloAnamneseSgQuestaoORepo.Atualizar(modeloAnamneseSgO);
            await Comitar();
        }

        public async Task<(bool criado, int modeloAnamneseSgQuestaoOId)> CriarOuAtualizar(CriarModeloAnamneseSgQuestaoOInputModel modeloAnamneseSgQuestaoO, bool atualizaSeExistir)
        {
            var cModeloAnamneseSgQuestaoO = (await _modeloAnamneseSgQuestaoORepo.Buscar(
                x => x.ModeloAnamneseSgQuestaoOid == modeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoOid
            )).FirstOrDefault();

            if (cModeloAnamneseSgQuestaoO == null)
            {
                cModeloAnamneseSgQuestaoO = ModeloAnamneseSgQuestaoO.CriarParaImportacao(
                    modeloAnamneseGID: modeloAnamneseSgQuestaoO.ModeloAnamneseGid,
                    modeloAnamneseSgID: modeloAnamneseSgQuestaoO.ModeloAnamneseSgid,
                    modeloAnamneseSgQuestaoID: modeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoId,
                    texto: modeloAnamneseSgQuestaoO.Texto,
                    ordem: modeloAnamneseSgQuestaoO.Ordem,
                    ativo: modeloAnamneseSgQuestaoO.Ativo
                );
                await Salvar(cModeloAnamneseSgQuestaoO);
                return (true, cModeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoOid); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cModeloAnamneseSgQuestaoO.AtualizarPropriedades(
                    modeloAnamneseGID: modeloAnamneseSgQuestaoO.ModeloAnamneseGid,
                    modeloAnamneseSgID: modeloAnamneseSgQuestaoO.ModeloAnamneseSgid,
                    modeloAnamneseSgQuestaoID: modeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoId,
                    texto: modeloAnamneseSgQuestaoO.Texto,
                    ordem: modeloAnamneseSgQuestaoO.Ordem,
                    ativo: modeloAnamneseSgQuestaoO.Ativo
                );
                await _modeloAnamneseSgQuestaoORepo.Atualizar(cModeloAnamneseSgQuestaoO);
                await Atualizar(cModeloAnamneseSgQuestaoO);
            }

            return (false, modeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoOid);
        }

        public async Task CriarParaImportacao(int modeloAnamneseSgQuestaoOID, int modeloAnamneseGID, int modeloAnamneseSgID, int modeloAnamneseSqQuestaoID, string texto, short ordem, bool? ativo)
        {
            var cModeloAnamneseSgQuestaoO = (await _modeloAnamneseSgQuestaoORepo.Buscar(
                            x => x.ModeloAnamneseSgQuestaoOid == modeloAnamneseSgQuestaoOID)
                            ).FirstOrDefault();
            if (cModeloAnamneseSgQuestaoO == null)
            {
                cModeloAnamneseSgQuestaoO = ModeloAnamneseSgQuestaoO.CriarParaImportacao(modeloAnamneseGID, modeloAnamneseSgID, modeloAnamneseSqQuestaoID, texto, ordem, ativo);
                await Salvar(cModeloAnamneseSgQuestaoO);
            }
            return;
        }

        public async Task Validar(int modeloAnamneseSgQuestaoOID)
        {
            var cModeloAnamneseSgQuestaoO = (await _modeloAnamneseSgQuestaoORepo.Buscar(x => x.ModeloAnamneseSgQuestaoOid == modeloAnamneseSgQuestaoOID)).FirstOrDefault();
            if (cModeloAnamneseSgQuestaoO == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Opção da questão do sub grupo do modelo de anamnese com ID {modeloAnamneseSgQuestaoOID} não encontrado."
                );
            }
        }
    }
}
