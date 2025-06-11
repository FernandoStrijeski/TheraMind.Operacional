using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.OrientacoesSexuais
{
    public class OrientacaoSexualServico : ServicoBase, IOrientacaoSexualServico
    {
        private IConfiguration _configuration;
        private IOrientacaoSexualRepo _orientacaoSexualRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public OrientacaoSexualServico(
            IConfiguration configuration,
            IOrientacaoSexualRepo orientacaoSexualRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _orientacaoSexualRepo = orientacaoSexualRepo;
        }

        public async Task<OrientacaoSexual>? BuscarPorID(int orientacaoSexualID) => await _orientacaoSexualRepo.BuscarPorID(orientacaoSexualID);

        public async Task<List<OrientacaoSexual>> BuscarTodos()
        {
            return await _orientacaoSexualRepo.BuscarFiltros();
        }

        public async Task<List<OrientacaoSexual>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _orientacaoSexualRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<OrientacaoSexual> Adicionar(OrientacaoSexual orientacaoSexual)
        {
            await _orientacaoSexualRepo.Adicionar(orientacaoSexual);
            await Comitar();
            return orientacaoSexual;
        }

        public async Task<OrientacaoSexual> Atualizar(OrientacaoSexual orientacaoSexual)
        {
            await _orientacaoSexualRepo.Atualizar(orientacaoSexual);
            await Comitar();
            return orientacaoSexual;
        }

        public async Task Deletar(int orientacaoSexualID)
        {
            var orientacaoSexual = _orientacaoSexualRepo.BuscarPorID(orientacaoSexualID).Result;

            if (orientacaoSexual == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Orientação sexual não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _orientacaoSexualRepo.Deletar(orientacaoSexualID);
            await Comitar();

            return;
        }
    }
}
