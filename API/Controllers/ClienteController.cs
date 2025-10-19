using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Clientes;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(1.0)]
    [RequerValidacaoDeToken]
    public class ClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteServico _clienteServico;
        private readonly IHttpContextAccessor _httpContext;

        public ClienteController(
            IMapper mapper,
            IClienteServico clienteServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _clienteServico = clienteServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o cliente a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o cliente pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ClienteViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid clienteID)
        {
            Cliente? cliente = await _clienteServico.BuscarPorID(clienteID);
            if (cliente == null)
                return NotFound("Nenhum cliente encontrado");

            var resultado = _mapper.Map<ClienteViewModel>(cliente);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os clientes pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ClienteViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var clientes = await _clienteServico.BuscarPorNome(parametro);

            if (clientes == null || clientes.Count == 0)
                return NotFound("Nenhum cliente encontrado");

            var resultado = _mapper.Map<List<ClienteViewModel>>(clientes);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ClienteViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var clientes = await _clienteServico.BuscarTodos();

            if (clientes == null || clientes.Count == 0)
                return NotFound("Nenhum cliente encontrado");


            var resultado = _mapper.Map<List<ClienteViewModel>>(clientes);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um cliente.
        /// </summary>         
        ///<response code="201">Cliente criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarClienteInputModel cliente)
        {
            var retorno = await _clienteServico.Adicionar(_mapper.Map<Cliente>(cliente));
            return Ok(_mapper.Map<ClienteViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>         
        ///<response code="200">Cliente atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ClienteViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ClienteInputModel cliente)
        {
            // Busca o registro existente
            var clienteExistente = await _clienteServico.BuscarPorID(cliente.ClienteId);
            if (clienteExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(cliente, clienteExistente); // Faz o merge

            var retorno = await _clienteServico.Atualizar(_mapper.Map<Cliente>(clienteExistente));
            return Ok(_mapper.Map<ClienteInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>         
        ///<response code="200">Cliente excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid clienteID)
        {
            await _clienteServico.Deletar(clienteID);
            return Ok();
        }
    }
}
