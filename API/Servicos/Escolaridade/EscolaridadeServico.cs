using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Escolaridades
{
    public class EscolaridadeServico : ServicoBase, IEscolaridadeServico
    {
        private IConfiguration _configuration;
        private IEscolaridadeRepo _escolaridadeRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EscolaridadeServico(
            IConfiguration configuration,
            IEscolaridadeRepo escolaridadeRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _escolaridadeRepo = escolaridadeRepo;
        }

        public async Task<Escolaridade>? BuscarPorID(int escolaridadeID) => await _escolaridadeRepo.BuscarPorID(escolaridadeID);

        public async Task<List<Escolaridade>> BuscarTodos()
        {
            return await _escolaridadeRepo.BuscarFiltros();
        }

        public async Task<List<Escolaridade>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _escolaridadeRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Escolaridade> Adicionar(Escolaridade escolaridade)
        {
            await _escolaridadeRepo.Adicionar(escolaridade);
            await Comitar();
            return escolaridade;
        }

        public async Task<Escolaridade> Atualizar(Escolaridade escolaridade)
        {
            await _escolaridadeRepo.Atualizar(escolaridade);
            await Comitar();
            return escolaridade;
        }

        public async Task Deletar(int id)
        {
            var escolaridade = _escolaridadeRepo.BuscarPorID(id).Result;

            if (escolaridade == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Escolaridade não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _escolaridadeRepo.Deletar(id);
            await Comitar();

            return;
        }
    }
}
