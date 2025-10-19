using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using BoletoNetCore;
using Dominio.Clientes;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace API.Servicos.Clientes
{
    public class ClienteServico : ServicoBase, IClienteServico
    {
        private IConfiguration _configuration;
        private IClienteRepo _clienteRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ClienteServico(
            IConfiguration configuration,
            IClienteRepo clienteRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _clienteRepo = clienteRepo;
        }

        public async Task<Cliente>? BuscarPorID(Guid clienteID) => await _clienteRepo.BuscarPorID(clienteID);

        public async Task<List<Cliente>> BuscarTodos()
        {
            return await _clienteRepo.BuscarFiltros();
        }

        public async Task<List<Cliente>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _clienteRepo.BuscarFiltros(x => x.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            await _clienteRepo.Adicionar(cliente);
            await Comitar();
            return cliente;
        }

        public async Task<Cliente> Atualizar(Cliente cliente)
        {
            await _clienteRepo.Atualizar(cliente);
            await Comitar();
            return cliente;
        }

        public async Task Deletar(Guid clienteID)
        {
            var cliente = _clienteRepo.BuscarPorID(clienteID).Result;

            if (cliente == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Cliente não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _clienteRepo.Deletar(clienteID);
            await Comitar();

            return;
        }
    }
}
