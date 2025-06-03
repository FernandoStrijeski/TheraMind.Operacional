using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasSessoes;
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
    public class AgendaSessaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaSessaoServico _agendaSessaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaSessaoController(
            IMapper mapper,
            IAgendaSessaoServico agendaSessaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaSessaoServico = agendaSessaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a sessão da agenda a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a sessão da agenda pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(AgendaSessaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid agendaSessaoID)
        {
            AgendaSessao? agendaSessao = await _agendaSessaoServico.BuscarPorID(agendaSessaoID);
            if (agendaSessao == null)
                return NotFound("Nenhuma sessão da agenda encontrada");


            var resultado = _mapper.Map<AgendaSessaoViewModel>(agendaSessao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as sessões da agenda
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var agendaSessoes = await _agendaSessaoServico.BuscarPorNome(parametro);

            if (agendaSessoes == null || agendaSessoes.Count == 0)
                return NotFound("Nenhuma sessão da agenda encontrada");

            var resultado = _mapper.Map<List<AgendaSessaoViewModel>>(agendaSessoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as sessões da agenda
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendaSessoes = await _agendaSessaoServico.BuscarTodos();

            if (agendaSessoes == null || agendaSessoes.Count == 0)
                return NotFound("Nenhuma sessão da agenda encontrada");


            var resultado = _mapper.Map<List<AgendaSessaoViewModel>>(agendaSessoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma sessão da agenda
        /// </summary>
        /// <response code="202">Sessão da agenda criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Sessão da agenda atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAgendaSessaoInputModel body)
        {
            var (criou, agendaSessaoId) = await _agendaSessaoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AgendaSessaoIdResponseViewModel(agendaSessaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
