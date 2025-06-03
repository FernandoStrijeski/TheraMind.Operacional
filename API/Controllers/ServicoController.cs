using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Servicos;
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
    public class ServicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicoServico _servicoServico;
        private readonly IHttpContextAccessor _httpContext;

        public ServicoController(
            IMapper mapper,
            IServicoServico servicoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _servicoServico = servicoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca um serviço a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o serviço pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ServicoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int servicoID)
        {
            Servico? servico = await _servicoServico.BuscarPorID(servicoID);
            if (servico == null)
                return NotFound("Nenhum serviço encontrado");


            var resultado = _mapper.Map<ServicoViewModel>(servico);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os serviços pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<ServicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var servicos = await _servicoServico.BuscarPorNome(parametro);

            if (servicos == null || servicos.Count == 0)
                return NotFound("Nenhum serviço encontrado");

            var resultado = _mapper.Map<List<ServicoViewModel>>(servicos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os serviços
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ServicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var servicos = await _servicoServico.BuscarTodos();

            if (servicos == null || servicos.Count == 0)
                return NotFound("Nenhum serviço encontrado");


            var resultado = _mapper.Map<List<ServicoViewModel>>(servicos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um serviço
        /// </summary>
        /// <response code="202">Serviço criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Serviço atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]

        [ProducesResponseType(typeof(ServicoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarServicoInputModel body)
        {
            var (criou, servicoId) = await _servicoServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new ServicoIdResponseViewModel(servicoId));

            return NoContent(); // Atualizado com sucesso, sem corpo 
        }
    }
}
