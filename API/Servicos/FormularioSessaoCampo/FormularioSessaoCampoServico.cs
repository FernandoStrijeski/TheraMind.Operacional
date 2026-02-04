using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.FormulariosSessaoCampos;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
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

        public async Task<FormularioSessaoCampo>? BuscarPorID(int formularioSessaoCampoID) => await _formularioSessaoCampoRepo.BuscarPorID(formularioSessaoCampoID);

        public async Task<List<FormularioSessaoCampo>> BuscarTodos()
        {
            return await _formularioSessaoCampoRepo.BuscarFiltros();
        }

        public async Task<List<FormularioSessaoCampo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _formularioSessaoCampoRepo.BuscarFiltros(x => x.NomeCampo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<FormularioSessaoCampo> Adicionar(FormularioSessaoCampo formularioSessaoCampo)
        {
            await _formularioSessaoCampoRepo.Adicionar(formularioSessaoCampo);
            await Comitar();
            return formularioSessaoCampo;
        }

        public async Task<FormularioSessaoCampo> Atualizar(FormularioSessaoCampo formularioSessaoCampo)
        {
            await _formularioSessaoCampoRepo.Atualizar(formularioSessaoCampo);
            await Comitar();
            return formularioSessaoCampo;
        }

        public async Task Deletar(int formularioSessaoCampoID)
        {
            var formularioSessaoCampo = _formularioSessaoCampoRepo.BuscarPorID(formularioSessaoCampoID).Result;

            if (formularioSessaoCampo == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Campo da sessão do formulário não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _formularioSessaoCampoRepo.Deletar(formularioSessaoCampoID);
            await Comitar();

            return;
        }
    }
}
