using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Convenios;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Convenios
{
    public class ConvenioServico : ServicoBase, IConvenioServico
    {
        private IConfiguration _configuration;
        private IConvenioRepo _convenioRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ConvenioServico(
            IConfiguration configuration,
            IConvenioRepo convenioRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _convenioRepo = convenioRepo;
        }

        public async Task<Convenio>? BuscarPorID(int convenioID) => await _convenioRepo.BuscarPorID(convenioID);

        public async Task<List<Convenio>> BuscarTodos()
        {
            return await _convenioRepo.BuscarFiltros();
        }

        public async Task<List<Convenio>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _convenioRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Convenio> Adicionar(Convenio convenio)
        {
            await _convenioRepo.Adicionar(convenio);
            await Comitar();
            return convenio;
        }

        public async Task<Convenio> Atualizar(Convenio convenio)
        {
            await _convenioRepo.Atualizar(convenio);
            await Comitar();
            return convenio;
        }

        public async Task Deletar(int convenioID)
        {
            var convenio = _convenioRepo.BuscarPorID(convenioID).Result;

            if (convenio == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Convênio não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _convenioRepo.Deletar(convenioID);
            await Comitar();

            return;
        }
    }
}
