using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Dominio.ModelosAnamneseSGQuestoes;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.ModelosAnamneseSGQuestoes
{
    public class ModeloAnamneseSgQuestaoServico : ServicoBase, IModeloAnamneseSgQuestaoServico
    {
        private IConfiguration _configuration;
        private IModeloAnamneseSgQuestaoRepo _modeloAnamneseSgQuestaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ModeloAnamneseSgQuestaoServico(
            IConfiguration configuration,
            IModeloAnamneseSgQuestaoRepo modeloAnamneseSgQuestaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _modeloAnamneseSgQuestaoRepo = modeloAnamneseSgQuestaoRepo;
        }

        public async Task<ModeloAnamneseSgQuestao>? BuscarPorID(int modeloAnamneseSgID) => await _modeloAnamneseSgQuestaoRepo.BuscarPorID(modeloAnamneseSgID);

        public async Task<List<ModeloAnamneseSgQuestao>> BuscarTodos()
        {
            return await _modeloAnamneseSgQuestaoRepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseSgQuestao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseSgQuestaoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(ModeloAnamneseSgQuestao modeloAnamneseSg)
        {
            await _modeloAnamneseSgQuestaoRepo.Adicionar(modeloAnamneseSg);
            await Comitar();
        }

        private async Task Atualizar(ModeloAnamneseSgQuestao modeloAnamneseSg)
        {
            await _modeloAnamneseSgQuestaoRepo.Atualizar(modeloAnamneseSg);
            await Comitar();
        }

        public async Task<(bool criado, int modeloAnamneseSgId)> CriarOuAtualizar(CriarModeloAnamneseSgQuestaoInputModel modeloAnamneseSgQuestao, bool atualizaSeExistir)
        {
            var cModeloAnamneseSgQuestao = (await _modeloAnamneseSgQuestaoRepo.Buscar(
                x => x.ModeloAnamneseSgQuestaoId == modeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoId
            )).FirstOrDefault();

            if (cModeloAnamneseSgQuestao == null)
            {
                cModeloAnamneseSgQuestao = ModeloAnamneseSgQuestao.CriarParaImportacao(
                    modeloAnamneseGID: modeloAnamneseSgQuestao.ModeloAnamneseGid,
                    modeloAnamneseSgID: modeloAnamneseSgQuestao.ModeloAnamneseSgid,
                    titulo: modeloAnamneseSgQuestao.Titulo,
                    tipoOpcao: modeloAnamneseSgQuestao.TipoOpcao,
                    ordem: modeloAnamneseSgQuestao.Ordem,
                    ativo: modeloAnamneseSgQuestao.Ativo
                );
                await Salvar(cModeloAnamneseSgQuestao);
                return (true, cModeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cModeloAnamneseSgQuestao.AtualizarPropriedades(
                    modeloAnamneseGID: modeloAnamneseSgQuestao.ModeloAnamneseGid,
                    modeloAnamneseSgID: modeloAnamneseSgQuestao.ModeloAnamneseSgid,                    
                    titulo: modeloAnamneseSgQuestao.Titulo,
                    tipoOpcao: modeloAnamneseSgQuestao.TipoOpcao,
                    ordem: modeloAnamneseSgQuestao.Ordem,
                    ativo: modeloAnamneseSgQuestao.Ativo
                );
                await _modeloAnamneseSgQuestaoRepo.Atualizar(cModeloAnamneseSgQuestao);
                await Atualizar(cModeloAnamneseSgQuestao);
            }

            return (false, modeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoId);
        }


        public async Task CriarParaImportacao(int modeloAnamneseSgQuestaoID, int modeloAnamneseGID, int modeloAnamneseSgID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            var cModeloAnamneseSgQuestao = (await _modeloAnamneseSgQuestaoRepo.Buscar(
                            x => x.ModeloAnamneseSgQuestaoId == modeloAnamneseSgQuestaoID)
                            ).FirstOrDefault();
            if (cModeloAnamneseSgQuestao == null)
            {
                cModeloAnamneseSgQuestao = ModeloAnamneseSgQuestao.CriarParaImportacao(modeloAnamneseGID, modeloAnamneseSgID, titulo, tipoOpcao, ordem, ativo);
                await Salvar(cModeloAnamneseSgQuestao);
            }
            return;
        }

        public async Task Validar(int modeloAnamneseSgQuestaoID)
        {
            var cModeloAnamneseSgQuestao = (await _modeloAnamneseSgQuestaoRepo.Buscar(x => x.ModeloAnamneseSgQuestaoId == modeloAnamneseSgQuestaoID)).FirstOrDefault();
            if (cModeloAnamneseSgQuestao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Questão do sub grupo do modelo de anamnese com ID {modeloAnamneseSgQuestaoID} não encontrado."
                );
            }
        }
    }
}
