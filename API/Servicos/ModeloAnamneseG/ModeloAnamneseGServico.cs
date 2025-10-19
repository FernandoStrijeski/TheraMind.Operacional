using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseG;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.ModelosAnamneseG
{
    public class ModeloAnamneseGServico : ServicoBase, IModeloAnamneseGServico
    {
        private IConfiguration _configuration;
        private IModeloAnamneseGRepo _modeloAnamneseGRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ModeloAnamneseGServico(
            IConfiguration configuration,
            IModeloAnamneseGRepo modeloAnamneseGRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _modeloAnamneseGRepo = modeloAnamneseGRepo;
        }

        public async Task<ModeloAnamneseG>? BuscarPorID(int modeloAnamneseGID) => await _modeloAnamneseGRepo.BuscarPorID(modeloAnamneseGID);

        public async Task<List<ModeloAnamneseG>> BuscarTodos()
        {
            return await _modeloAnamneseGRepo.BuscarFiltros();
        }

        public async Task<List<ModeloAnamneseG>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _modeloAnamneseGRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<ModeloAnamneseG> Adicionar(ModeloAnamneseG modeloAnamneseG)
        {
            await _modeloAnamneseGRepo.Adicionar(modeloAnamneseG);
            await Comitar();
            return modeloAnamneseG;
        }

        public async Task<ModeloAnamneseG> Atualizar(ModeloAnamneseG modeloAnamneseG)
        {
            await _modeloAnamneseGRepo.Atualizar(modeloAnamneseG);
            await Comitar();
            return modeloAnamneseG;
        }

        public async Task Deletar(int modeloAnamneseGID)
        {
            var modeloAnamneseG = _modeloAnamneseGRepo.BuscarPorID(modeloAnamneseGID).Result;

            if (modeloAnamneseG == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Grupo do modelo de amanmese não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _modeloAnamneseGRepo.Deletar(modeloAnamneseGID);
            await Comitar();

            return;
        }
    }
}
