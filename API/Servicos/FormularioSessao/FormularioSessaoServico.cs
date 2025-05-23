using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.FormulariosSessoes;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.FormulariosSessoes
{
    public class FormularioSessaoServico : ServicoBase, IFormularioSessaoServico
    {
        private IConfiguration _configuration;
        private IFormularioSessaoRepo _formularioSessaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public FormularioSessaoServico(
        IConfiguration configuration,
            IFormularioSessaoRepo formularioSessaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _formularioSessaoRepo = formularioSessaoRepo;
        }

        public async Task<FormularioSessao>? BuscarPorID(int formularioSessaoID) => await _formularioSessaoRepo.BuscarPorID(formularioSessaoID);

        public async Task<List<FormularioSessao>> BuscarTodos()
        {
            return await _formularioSessaoRepo.BuscarFiltros();
        }

        public async Task<List<FormularioSessao>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _formularioSessaoRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(FormularioSessao formularioSessao)
        {
            await _formularioSessaoRepo.Adicionar(formularioSessao);
            await Comitar();
        }

        private async Task Atualizar(FormularioSessao formularioSessao)
        {
            await _formularioSessaoRepo.Atualizar(formularioSessao);
            await Comitar();
        }

        public async Task<(bool criado, int formularioSessaoId)> CriarOuAtualizar(CriarFormularioSessaoInputModel formularioSessao, bool atualizaSeExistir)
        {
            var cFormularioSessao = (await _formularioSessaoRepo.Buscar(
                x => x.FormularioSessaoId == formularioSessao.FormularioSessaoId
            )).FirstOrDefault();

            if (cFormularioSessao == null)
            {
                cFormularioSessao = FormularioSessao.CriarParaImportacao(
                    empresaID: formularioSessao.EmpresaId,
                    filialID: formularioSessao.FilialId,
                    servicoID: formularioSessao.ServicoId,                    
                    nome: formularioSessao.Nome,                    
                    ativo: formularioSessao.Ativo
                );
                await Salvar(cFormularioSessao);
                return (true, cFormularioSessao.FormularioSessaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cFormularioSessao.AtualizarPropriedades(
                    empresaID: formularioSessao.EmpresaId,
                    filialID: formularioSessao.FilialId,
                    servicoID: formularioSessao.ServicoId,
                    nome: formularioSessao.Nome,
                    ativo: formularioSessao.Ativo
                );
                await _formularioSessaoRepo.Atualizar(cFormularioSessao);
                await Atualizar(cFormularioSessao);
            }

            return (false, formularioSessao.FormularioSessaoId);
        }


        public async Task CriarParaImportacao(int formularioSessaoID, Guid empresaID, int filialID, int servicoID, string nome, bool? ativo)
        {
            var cFormularioSessao = (await _formularioSessaoRepo.Buscar(
                            x => x.FormularioSessaoId == formularioSessaoID)
                            ).FirstOrDefault();
            if (cFormularioSessao == null)
            {
                cFormularioSessao = FormularioSessao.CriarParaImportacao(empresaID, filialID, servicoID, nome, ativo);
                await Salvar(cFormularioSessao);
            }
            return;
        }

        public async Task Validar(int formularioSessaoID)
        {
            var cFormularioSessao = (await _formularioSessaoRepo.Buscar(x => x.FormularioSessaoId == formularioSessaoID)).FirstOrDefault();
            if (cFormularioSessao == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Formulario da sessão com ID {formularioSessaoID} não encontrado."
                );
            }
        }
    }
}
