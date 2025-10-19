using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.Planos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Planos
{
    public class PlanoServico : ServicoBase, IPlanoServico
    {
        private IConfiguration _configuration;
        private IPlanoRepo _planoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PlanoServico(
            IConfiguration configuration,
            IPlanoRepo planoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _planoRepo = planoRepo;
        }
        
        public async Task<Plano>? BuscarPorID(Guid planoID) => await _planoRepo.BuscarPorID(planoID);

        public async Task<List<Plano>> BuscarTodos()
        {
            return await _planoRepo.BuscarFiltros();
        }

        public async Task<List<Plano>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _planoRepo.BuscarFiltros(x => x.NomePlano.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Plano> Adicionar(Plano plano)
        {
            await _planoRepo.Adicionar(plano);
            await Comitar();
            return plano;
        }

        public async Task<Plano> Atualizar(Plano plano)
        {
            await _planoRepo.Atualizar(plano);
            await Comitar();
            return plano;
        }

        public async Task Deletar(Guid planoID)
        {
            var plano = _planoRepo.BuscarPorID(planoID).Result;

            if (plano == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Plano não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _planoRepo.Deletar(planoID);
            await Comitar();

            return;
        }
    }
}
