using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.GrauParentescos
{
    public class GrauParentescoServico : ServicoBase, IGrauParentescoServico
    {
        private IConfiguration _configuration;
        private IGrauParentescoRepo _grauParentescoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public GrauParentescoServico(
            IConfiguration configuration,
            IGrauParentescoRepo grauParentescoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _grauParentescoRepo = grauParentescoRepo;
        }

        public async Task<GrauParentesco>? BuscarPorID(int grauParentescoID) => await _grauParentescoRepo.BuscarPorID(grauParentescoID);

        public async Task<List<GrauParentesco>> BuscarTodos()
        {
            return await _grauParentescoRepo.BuscarFiltros();
        }

        public async Task<List<GrauParentesco>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _grauParentescoRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<GrauParentesco> Adicionar(GrauParentesco grauParentesco)
        {
            await _grauParentescoRepo.Adicionar(grauParentesco);
            await Comitar();
            return grauParentesco;
        }

        public async Task<GrauParentesco> Atualizar(GrauParentesco grauParentesco)
        {
            await _grauParentescoRepo.Atualizar(grauParentesco);
            await Comitar();
            return grauParentesco;
        }

        public async Task Deletar(int grauParentescoID)
        {
            var grauParentesco = _grauParentescoRepo.BuscarPorID(grauParentescoID).Result;

            if (grauParentesco == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Grau de parentesco não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _grauParentescoRepo.Deletar(grauParentescoID);
            await Comitar();

            return;
        }
    }
}
