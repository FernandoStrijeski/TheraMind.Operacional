using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.IdentidadesGeneros
{
    public class IdentidadeGeneroServico : ServicoBase, IIdentidadeGeneroServico
    {
        private IConfiguration _configuration;
        private IIdentidadeGeneroRepo _identidadeGeneroRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public IdentidadeGeneroServico(
            IConfiguration configuration,
            IIdentidadeGeneroRepo identidadeGeneroRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _identidadeGeneroRepo = identidadeGeneroRepo;
        }

        public async Task<IdentidadeGenero>? BuscarPorID(int identidadeGeneroID) => await _identidadeGeneroRepo.BuscarPorID(identidadeGeneroID);

        public async Task<List<IdentidadeGenero>> BuscarTodos()
        {
            return await _identidadeGeneroRepo.BuscarFiltros();
        }

        public async Task<List<IdentidadeGenero>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _identidadeGeneroRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<IdentidadeGenero> Adicionar(IdentidadeGenero identidadeGenero)
        {
            await _identidadeGeneroRepo.Adicionar(identidadeGenero);
            await Comitar();
            return identidadeGenero;
        }

        public async Task<IdentidadeGenero> Atualizar(IdentidadeGenero identidadeGenero)
        {
            await _identidadeGeneroRepo.Atualizar(identidadeGenero);
            await Comitar();
            return identidadeGenero;
        }

        public async Task Deletar(int identidadeGeneroID)
        {
            var identidadeGenero = _identidadeGeneroRepo.BuscarPorID(identidadeGeneroID).Result;

            if (identidadeGenero == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Identidade de gênero não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _identidadeGeneroRepo.Deletar(identidadeGeneroID);
            await Comitar();

            return;
        }
    }
}
