using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasProfissionais;
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
    public class AgendaProfissionalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaProfissionalServico _agendaProfissionalServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaProfissionalController(
            IMapper mapper,
            IAgendaProfissionalServico agendaProfissionalServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaProfissionalServico = agendaProfissionalServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a agenda do profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a agenda do profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AgendaProfissionalViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int agendaProfissionalID)
        {
            AgendaProfissional? agendaProfissional = await _agendaProfissionalServico.BuscarPorID(agendaProfissionalID);
            if (agendaProfissional == null)
                return NotFound("Nenhuma agenda do profissional encontrada");


            var resultado = _mapper.Map<AgendaProfissionalViewModel>(agendaProfissional);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as agendas dos profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AgendaProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendasProfissionais = await _agendaProfissionalServico.BuscarTodos();

            if (agendasProfissionais == null || agendasProfissionais.Count == 0)
                return NotFound("Nenhuma agenda do profissional encontrada");


            var resultado = _mapper.Map<List<AgendaProfissionalViewModel>>(agendasProfissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma agenda do profissional
        /// </summary>
        /// <response code="202">Agenda do profissional criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Agenda do profissional atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaProfissionalIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAgendaProfissionalInputModel body)
        {
            var (criou, agendaProfissionalId) = await _agendaProfissionalServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AgendaProfissionalIdResponseViewModel(agendaProfissionalId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
