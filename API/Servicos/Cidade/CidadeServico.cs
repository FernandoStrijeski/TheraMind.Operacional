using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Cidades
{
    public class CidadeServico : ServicoBase, ICidadeServico
    {
        private IConfiguration _configuration;
        private ICidadeRepo _cidadeRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public CidadeServico(
            IConfiguration configuration,
            ICidadeRepo cidadeRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _cidadeRepo = cidadeRepo;
        }

        public async Task<Cidade>? BuscarPorID(int cidadeId) => await _cidadeRepo.BuscarPorID(cidadeId);

        public async Task<List<Cidade>> BuscarTodos()
        {
            return await _cidadeRepo.BuscarFiltros();
        }

        public async Task<List<Cidade>> BuscarTodasPorEstado(string UF)
        {
            return await _cidadeRepo.BuscarFiltros(x => x.Uf.Contains(UF.ToUpper()));
        }

        public async Task<List<Cidade>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _cidadeRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Cidade>? BuscarPorIBGE(int codigoIBGE) => await _cidadeRepo.BuscarPorIBGE(codigoIBGE);

        public async Task<Cidade> Adicionar(Cidade cidade)
        {
            await _cidadeRepo.Adicionar(cidade);
            await Comitar();
            return cidade;
        }

        public async Task<Cidade> Atualizar(Cidade cidade)
        {
            await _cidadeRepo.Atualizar(cidade);
            await Comitar();
            return cidade;
        }

        public async Task Deletar(int cidadeId)
        {
            var cidade = _cidadeRepo.BuscarPorID(cidadeId).Result;

            if (cidade == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Cidade não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _cidadeRepo.Deletar(cidadeId);
            await Comitar();

            return;
        }
    }
}
