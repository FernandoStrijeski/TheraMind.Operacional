using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.ModelosAnamneseG;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(ModeloAnamneseG modeloAnamneseG)
        {
            await _modeloAnamneseGRepo.Adicionar(modeloAnamneseG);
            await Comitar();
        }

        private async Task Atualizar(ModeloAnamneseG modeloAnamneseG)
        {
            await _modeloAnamneseGRepo.Atualizar(modeloAnamneseG);
            await Comitar();
        }

        public async Task<(bool criado, int modeloAnamneseGId)> CriarOuAtualizar(CriarModeloAnamneseGInputModel modeloAnamneseG, bool atualizaSeExistir)
        {
            var cModeloAnamneseG = (await _modeloAnamneseGRepo.Buscar(
                x => x.ModeloAnamneseGid == modeloAnamneseG.ModeloAnamneseGid
            )).FirstOrDefault();

            if (cModeloAnamneseG == null)
            {
                cModeloAnamneseG = ModeloAnamneseG.CriarParaImportacao(
                    titulo: modeloAnamneseG.Titulo,
                    privado: modeloAnamneseG.Privado,
                    editadoPorTodos: modeloAnamneseG.EditadoPorTodos,
                    ativo: modeloAnamneseG.Ativo
                );
                await Salvar(cModeloAnamneseG);
                return (true, cModeloAnamneseG.ModeloAnamneseGid); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cModeloAnamneseG.AtualizarPropriedades(
                    titulo: modeloAnamneseG.Titulo,
                    privado: modeloAnamneseG.Privado,
                    editadoPorTodos: modeloAnamneseG.EditadoPorTodos,
                    ativo: modeloAnamneseG.Ativo
                );
                await _modeloAnamneseGRepo.Atualizar(cModeloAnamneseG);
                await Atualizar(cModeloAnamneseG);
            }

            return (false, modeloAnamneseG.ModeloAnamneseGid);
        }


        public async Task CriarParaImportacao(int modeloAnamneseGID, string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            var cModeloAnamneseG = (await _modeloAnamneseGRepo.Buscar(
                            x => x.ModeloAnamneseGid == modeloAnamneseGID)
                            ).FirstOrDefault();
            if (cModeloAnamneseG == null)
            {
                cModeloAnamneseG = ModeloAnamneseG.CriarParaImportacao(titulo, privado, editadoPorTodos, ativo);
                await Salvar(cModeloAnamneseG);
            }
            return;
        }

        public async Task Validar(int modeloAnamneseGID)
        {
            var cModeloAnamneseG = (await _modeloAnamneseGRepo.Buscar(x => x.ModeloAnamneseGid == modeloAnamneseGID)).FirstOrDefault();
            if (cModeloAnamneseG == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Modelo de anamnese com ID {modeloAnamneseGID} não encontrado."
                );
            }
        }
    }
}
