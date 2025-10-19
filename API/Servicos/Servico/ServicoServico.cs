using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.Servicos
{
    public class ServicoServico : ServicoBase, IServicoServico
    {
        private IConfiguration _configuration;
        private IServicoRepo _servicoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ServicoServico(
            IConfiguration configuration,
            IServicoRepo servicoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _servicoRepo = servicoRepo;
        }
        
        public async Task<Servico>? BuscarPorID(int servicoID) => await _servicoRepo.BuscarPorID(servicoID);

        public async Task<List<Servico>> BuscarTodos()
        {
            return await _servicoRepo.BuscarFiltros();
        }

        public async Task<List<Servico>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _servicoRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Servico> Adicionar(Servico servico)
        {
            await _servicoRepo.Adicionar(servico);
            await Comitar();
            return servico;
        }

        public async Task<Servico> Atualizar(Servico servico)
        {
            await _servicoRepo.Atualizar(servico);
            await Comitar();
            return servico;
        }

        public async Task Deletar(int servicoID)
        {
            var servico = _servicoRepo.BuscarPorID(servicoID).Result;

            if (servico == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Serviço não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _servicoRepo.Deletar(servicoID);
            await Comitar();

            return;
        }
    }
}
