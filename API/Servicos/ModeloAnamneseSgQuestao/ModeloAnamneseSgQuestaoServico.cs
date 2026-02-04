using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Dominio.ModelosAnamneseSGQuestoes;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
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

        public async Task<ModeloAnamneseSgQuestao>? BuscarPorID(int modeloAnamneseSgQuestaoID) => await _modeloAnamneseSgQuestaoRepo.BuscarPorID(modeloAnamneseSgQuestaoID);

        public async Task<List<ModeloAnamneseSgQuestao>> BuscarTodos()
        {
            return await _modeloAnamneseSgQuestaoRepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseSgQuestao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseSgQuestaoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<ModeloAnamneseSgQuestao> Adicionar(ModeloAnamneseSgQuestao modeloAnamneseSgQuestao)
        {
            await _modeloAnamneseSgQuestaoRepo.Adicionar(modeloAnamneseSgQuestao);
            await Comitar();
            return modeloAnamneseSgQuestao;
        }

        public async Task<ModeloAnamneseSgQuestao> Atualizar(ModeloAnamneseSgQuestao modeloAnamneseSgQuestao)
        {
            await _modeloAnamneseSgQuestaoRepo.Atualizar(modeloAnamneseSgQuestao);
            await Comitar();
            return modeloAnamneseSgQuestao;
        }

        public async Task Deletar(int modeloAnamneseSgQuestaoID)
        {
            var modeloAnamneseSgQuestao = _modeloAnamneseSgQuestaoRepo.BuscarPorID(modeloAnamneseSgQuestaoID).Result;

            if (modeloAnamneseSgQuestao == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Questão do subgrupo da amamnese não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _modeloAnamneseSgQuestaoRepo.Deletar(modeloAnamneseSgQuestaoID);
            await Comitar();

            return;
        }
    }
}
