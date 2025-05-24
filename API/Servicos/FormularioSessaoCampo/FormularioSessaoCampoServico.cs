using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.FormulariosSessaoCampos;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.FormularioSessaoCampos
{
    public class FormularioSessaoCampoServico : ServicoBase, IFormularioSessaoCampoServico
    {
        private IConfiguration _configuration;
        private IFormularioSessaoCampoRepo _formularioSessaoCampoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public FormularioSessaoCampoServico(
        IConfiguration configuration,
            IFormularioSessaoCampoRepo formularioSessaoCampoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _formularioSessaoCampoRepo = formularioSessaoCampoRepo;
        }

        public async Task<FormularioSessaoCampo>? BuscarPorID(int formularioSessaoID) => await _formularioSessaoCampoRepo.BuscarPorID(formularioSessaoID);

        public async Task<List<FormularioSessaoCampo>> BuscarTodos()
        {
            return await _formularioSessaoCampoRepo.BuscarFiltros();
        }

        public async Task<List<FormularioSessaoCampo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _formularioSessaoCampoRepo.BuscarFiltros(x => x.NomeCampo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(FormularioSessaoCampo formularioSessaoCampo)
        {
            await _formularioSessaoCampoRepo.Adicionar(formularioSessaoCampo);
            await Comitar();
        }

        private async Task Atualizar(FormularioSessaoCampo formularioSessaoCampo)
        {
            await _formularioSessaoCampoRepo.Atualizar(formularioSessaoCampo);
            await Comitar();
        }

        public async Task<(bool criado, int formularioSessaoCampoId)> CriarOuAtualizar(CriarFormularioSessaoCampoInputModel formularioSessaoCampo, bool atualizaSeExistir)
        {
            var cFormularioSessaoCampo = (await _formularioSessaoCampoRepo.Buscar(
                x => x.FormularioSessaoCampoId == formularioSessaoCampo.FormularioSessaoCampoId
            )).FirstOrDefault();

            if (cFormularioSessaoCampo == null)
            {
                cFormularioSessaoCampo = FormularioSessaoCampo.CriarParaImportacao(
                    empresaID: formularioSessaoCampo.EmpresaId,
                    filialID: formularioSessaoCampo.FilialId,
                    servicoID: formularioSessaoCampo.ServicoId,
                    formularioSessaoID: formularioSessaoCampo.FormularioSessaoId,
                    nomeCampo: formularioSessaoCampo.NomeCampo,
                    ativo: formularioSessaoCampo.Ativo
                );
                await Salvar(cFormularioSessaoCampo);
                return (true, cFormularioSessaoCampo.FormularioSessaoCampoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cFormularioSessaoCampo.AtualizarPropriedades(
                    empresaID: formularioSessaoCampo.EmpresaId,
                    filialID: formularioSessaoCampo.FilialId,
                    servicoID: formularioSessaoCampo.ServicoId,
                    formularioSessaoID: formularioSessaoCampo.FormularioSessaoId,
                    nomeCampo: formularioSessaoCampo.NomeCampo,
                    ativo: formularioSessaoCampo.Ativo
                );
                await _formularioSessaoCampoRepo.Atualizar(cFormularioSessaoCampo);
                await Atualizar(cFormularioSessaoCampo);
            }

            return (false, formularioSessaoCampo.FormularioSessaoCampoId);
        }


        public async Task CriarParaImportacao(int formularioSessaoCampoID, Guid empresaID, int filialID, int servicoID, int formularioSessaoID, string nomeCampo, bool? ativo)
        {
            var cFormularioSessaoCampo = (await _formularioSessaoCampoRepo.Buscar(
                            x => x.FormularioSessaoCampoId == formularioSessaoCampoID)
                            ).FirstOrDefault();
            if (cFormularioSessaoCampo == null)
            {
                cFormularioSessaoCampo = FormularioSessaoCampo.CriarParaImportacao(empresaID, filialID, servicoID, formularioSessaoID, nomeCampo, ativo);
                await Salvar(cFormularioSessaoCampo);
            }
            return;
        }

        public async Task Validar(int formularioSessaoCampoID)
        {
            var cFormularioSessaoCampo = (await _formularioSessaoCampoRepo.Buscar(x => x.FormularioSessaoCampoId == formularioSessaoCampoID)).FirstOrDefault();
            if (cFormularioSessaoCampo == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Campo do formulario da sessão com ID {formularioSessaoCampoID} não encontrado."
                );
            }
        }
    }
}
