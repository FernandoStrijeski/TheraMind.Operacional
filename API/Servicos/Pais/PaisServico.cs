using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Paises
{
    public class PaisServico : ServicoBase, IPaisServico
    {
        private IConfiguration _configuration;
        private IPaisRepo _paisRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PaisServico(
            IConfiguration configuration,
            IPaisRepo paisRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _paisRepo = paisRepo;
        }

        public async Task<Pais>? BuscarPorID(int paisID) => await _paisRepo.BuscarPorID(paisID);

        public async Task<List<Pais>> BuscarTodos()
        {
            return await _paisRepo.BuscarFiltros();
        }

        public async Task<List<Pais>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _paisRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Pais> Adicionar(Pais pais)
        {
            await _paisRepo.Adicionar(pais);
            await Comitar();
            return pais;
        }

        public async Task<Pais> Atualizar(Pais pais)
        {
            await _paisRepo.Atualizar(pais);
            await Comitar();
            return pais;
        }

        public async Task Deletar(int paisID)
        {
            var pais = _paisRepo.BuscarPorID(paisID).Result;

            if (pais == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "País não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _paisRepo.Deletar(paisID);
            await Comitar();

            return;
        }
    }
}
