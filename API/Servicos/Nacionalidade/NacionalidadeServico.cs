using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Nacionalidades
{
    public class NacionalidadeServico : ServicoBase, INacionalidadeServico
    {
        private IConfiguration _configuration;
        private INacionalidadeRepo _nacionalidadeRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public NacionalidadeServico(
            IConfiguration configuration,
            INacionalidadeRepo nacionalidadeRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _nacionalidadeRepo = nacionalidadeRepo;
        }

        public async Task<Nacionalidade>? BuscarPorID(int nacionalidadeID) => await _nacionalidadeRepo.BuscarPorID(nacionalidadeID);

        public async Task<List<Nacionalidade>> BuscarTodos()
        {
            return await _nacionalidadeRepo.BuscarFiltros();
        }

        public async Task<List<Nacionalidade>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _nacionalidadeRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Nacionalidade> Adicionar(Nacionalidade nacionalidade)
        {
            await _nacionalidadeRepo.Adicionar(nacionalidade);
            await Comitar();
            return nacionalidade;
        }

        public async Task<Nacionalidade> Atualizar(Nacionalidade nacionalidade)
        {
            await _nacionalidadeRepo.Atualizar(nacionalidade);
            await Comitar();
            return nacionalidade;
        }

        public async Task Deletar(int nacionalidadeID)
        {
            var nacionalidade = _nacionalidadeRepo.BuscarPorID(nacionalidadeID).Result;

            if (nacionalidade == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Nacionalidade não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _nacionalidadeRepo.Deletar(nacionalidadeID);
            await Comitar();

            return;
        }
    }
}
