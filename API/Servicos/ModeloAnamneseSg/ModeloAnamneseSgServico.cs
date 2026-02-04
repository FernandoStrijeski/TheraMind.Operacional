using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.ModelosAnamneseSG
{
    public class ModeloAnamneseSgServico : ServicoBase, IModeloAnamneseSgServico
    {
        private IConfiguration _configuration;
        private IModeloAnamneseSgRepo _modeloAnamneseSgGRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ModeloAnamneseSgServico(
            IConfiguration configuration,
            IModeloAnamneseSgRepo modeloAnamneseSgRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _modeloAnamneseSgGRepo = modeloAnamneseSgRepo;
        }

        public async Task<ModeloAnamneseSg>? BuscarPorID(int modeloAnamneseSgID) => await _modeloAnamneseSgGRepo.BuscarPorID(modeloAnamneseSgID);

        public async Task<List<ModeloAnamneseSg>> BuscarTodos()
        {
            return await _modeloAnamneseSgGRepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseSg>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseSgGRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<ModeloAnamneseSg> Adicionar(ModeloAnamneseSg modeloAnamneseSg)
        {
            await _modeloAnamneseSgGRepo.Adicionar(modeloAnamneseSg);
            await Comitar();
            return modeloAnamneseSg;
        }

        public async Task<ModeloAnamneseSg> Atualizar(ModeloAnamneseSg modeloAnamneseSg)
        {
            await _modeloAnamneseSgGRepo.Atualizar(modeloAnamneseSg);
            await Comitar();
            return modeloAnamneseSg;
        }

        public async Task Deletar(int modeloAnamneseSgID)
        {
            var modeloAnamneseSg = _modeloAnamneseSgGRepo.BuscarPorID(modeloAnamneseSgID).Result;

            if (modeloAnamneseSg == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Subgrupo do modelo de amanmese não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _modeloAnamneseSgGRepo.Deletar(modeloAnamneseSgID);
            await Comitar();

            return;
        }
    }
}
