using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasSessaoItens;
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
    public class AgendaSessaoItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaSessaoItemServico _agendaSessaoItemServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaSessaoItemController(
            IMapper mapper,
            IAgendaSessaoItemServico agendaSessaoItemServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaSessaoItemServico = agendaSessaoItemServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o item da sessão da agenda a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o item da sessão da agenda pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(AgendaSessaoItemViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int agendaSessaoItemID)
        {
            AgendaSessaoItem? agendaSessaoItem = await _agendaSessaoItemServico.BuscarPorID(agendaSessaoItemID);
            if (agendaSessaoItem == null)
                return NotFound("Nenhum item da sessão da agenda encontrada");


            var resultado = _mapper.Map<AgendaSessaoItemViewModel>(agendaSessaoItem);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os itens das sessões da agenda
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoItemViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var agendaSessaoItens = await _agendaSessaoItemServico.BuscarPorNome(parametro);

            if (agendaSessaoItens == null || agendaSessaoItens.Count == 0)
                return NotFound("Nenhum item da sessão da agenda encontrada");

            var resultado = _mapper.Map<List<AgendaSessaoItemViewModel>>(agendaSessaoItens);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os itens das sessões da agenda
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoItemViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendaSessoaoItens = await _agendaSessaoItemServico.BuscarTodos();

            if (agendaSessoaoItens == null || agendaSessoaoItens.Count == 0)
                return NotFound("Nenhum item da sessão da agenda encontrada");


            var resultado = _mapper.Map<List<AgendaSessaoItemViewModel>>(agendaSessoaoItens);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um item da sessão da agenda
        /// </summary>
        /// <response code="202">Item da sessão da agenda criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Item da sessão da agenda atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoItemIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAgendaSessaoItemInputModel body)
        {
            var (criou, agendaSessaoItemId) = await _agendaSessaoItemServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AgendaSessaoItemIdResponseViewModel(agendaSessaoItemId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
