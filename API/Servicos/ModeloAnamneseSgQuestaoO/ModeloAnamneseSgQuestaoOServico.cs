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

        public async Task<ModeloAnamneseSgQuestaoO>? BuscarPorID(int modeloAnamneseSgQuestaoOID) => await _modeloAnamneseSgQuestaoORepo.BuscarPorID(modeloAnamneseSgQuestaoOID);

        public async Task<List<ModeloAnamneseSgQuestaoO>> BuscarTodos()
        {
            return await _modeloAnamneseSgQuestaoORepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseSgQuestaoO>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseSgQuestaoORepo.BuscarFiltros(x => x.Texto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<ModeloAnamneseSgQuestaoO> Adicionar(ModeloAnamneseSgQuestaoO modeloAnamneseSgQuestaoO)
        {
            await _modeloAnamneseSgQuestaoORepo.Adicionar(modeloAnamneseSgQuestaoO);
            await Comitar();
            return modeloAnamneseSgQuestaoO;
        }

        public async Task<ModeloAnamneseSgQuestaoO> Atualizar(ModeloAnamneseSgQuestaoO modeloAnamneseSgQuestaoO)
        {
            await _modeloAnamneseSgQuestaoORepo.Atualizar(modeloAnamneseSgQuestaoO);
            await Comitar();
            return modeloAnamneseSgQuestaoO;
        }

        public async Task Deletar(int modeloAnamneseSgQuestaoOID)
        {
            var modeloAnamneseSgQuestaoO = _modeloAnamneseSgQuestaoORepo.BuscarPorID(modeloAnamneseSgQuestaoOID).Result;

            if (modeloAnamneseSgQuestaoO == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Opção da questão do subgrupo de anamnese não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _modeloAnamneseSgQuestaoORepo.Deletar(modeloAnamneseSgQuestaoOID);
            await Comitar();

            return;
        }
    }
}
