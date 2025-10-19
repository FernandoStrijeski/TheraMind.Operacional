using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.FormulariosSessoes;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
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

        public async Task<FormularioSessao> Adicionar(FormularioSessao formularioSessao)
        {
            await _formularioSessaoRepo.Adicionar(formularioSessao);
            await Comitar();
            return formularioSessao;
        }

        public async Task<FormularioSessao> Atualizar(FormularioSessao formularioSessao)
        {
            await _formularioSessaoRepo.Atualizar(formularioSessao);
            await Comitar();
            return formularioSessao;
        }

        public async Task Deletar(int formularioSessaoID)
        {
            var formularioSessao = _formularioSessaoRepo.BuscarPorID(formularioSessaoID).Result;

            if (formularioSessao == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Sessão do formulário não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _formularioSessaoRepo.Deletar(formularioSessaoID);
            await Comitar();

            return;
        }
    }
}
