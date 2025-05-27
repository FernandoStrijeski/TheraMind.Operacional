using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseSG;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(ModeloAnamneseSg modeloAnamneseSg)
        {
            await _modeloAnamneseSgGRepo.Adicionar(modeloAnamneseSg);
            await Comitar();
        }

        private async Task Atualizar(ModeloAnamneseSg modeloAnamneseSg)
        {
            await _modeloAnamneseSgGRepo.Atualizar(modeloAnamneseSg);
            await Comitar();
        }

        public async Task<(bool criado, int modeloAnamneseSgId)> CriarOuAtualizar(CriarModeloAnamneseSgInputModel modeloAnamneseSg, bool atualizaSeExistir)
        {
            var cModeloAnamneseSg = (await _modeloAnamneseSgGRepo.Buscar(
                x => x.ModeloAnamneseSgid == modeloAnamneseSg.ModeloAnamneseSgid
            )).FirstOrDefault();

            if (cModeloAnamneseSg == null)
            {
                cModeloAnamneseSg = ModeloAnamneseSg.CriarParaImportacao(
                    modeloanamneseGID: modeloAnamneseSg.ModeloAnamneseGid,
                    titulo: modeloAnamneseSg.Titulo,
                    ordem: modeloAnamneseSg.Ordem,
                    ativo: modeloAnamneseSg.Ativo
                );
                await Salvar(cModeloAnamneseSg);
                return (true, cModeloAnamneseSg.ModeloAnamneseSgid); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cModeloAnamneseSg.AtualizarPropriedades(
                    modeloanamneseGID: modeloAnamneseSg.ModeloAnamneseGid,
                    titulo: modeloAnamneseSg.Titulo,
                    ordem: modeloAnamneseSg.Ordem,
                    ativo: modeloAnamneseSg.Ativo
                );
                await _modeloAnamneseSgGRepo.Atualizar(cModeloAnamneseSg);
                await Atualizar(cModeloAnamneseSg);
            }

            return (false, modeloAnamneseSg.ModeloAnamneseSgid);
        }


        public async Task CriarParaImportacao(int modeloAnamneseSgID, int modeloAnamneseGID, string titulo, short ordem, bool? ativo)
        {
            var cModeloAnamneseSg = (await _modeloAnamneseSgGRepo.Buscar(
                            x => x.ModeloAnamneseGid == modeloAnamneseSgID)
                            ).FirstOrDefault();
            if (cModeloAnamneseSg == null)
            {
                cModeloAnamneseSg = ModeloAnamneseSg.CriarParaImportacao(modeloAnamneseGID, titulo, ordem, ativo);
                await Salvar(cModeloAnamneseSg);
            }
            return;
        }

        public async Task Validar(int modeloAnamneseSgID)
        {
            var cModeloAnamneseSg = (await _modeloAnamneseSgGRepo.Buscar(x => x.ModeloAnamneseSgid == modeloAnamneseSgID)).FirstOrDefault();
            if (cModeloAnamneseSg == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Sub grupo do modelo de anamnese com ID {modeloAnamneseSgID} não encontrado."
                );
            }
        }
    }
}
