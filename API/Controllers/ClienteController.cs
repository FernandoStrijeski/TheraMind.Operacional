using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Clientes;
using API.Servicos.Convenios;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("BuscarPorID")]
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
        [HttpGet("BuscarPorNome")]
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
        [HttpGet("Todos")]
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
        /// Cria ou atualiza um cliente
        /// </summary>
        /// <response code="202">Cliente criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Cliente atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN, GESTOR, PROFISSIONAL")]
        [ProducesResponseType(typeof(ClienteIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarClienteInputModel body)
        {
            var (criou, clienteId) = await _clienteServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ClienteIdResponseViewModel(clienteId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
