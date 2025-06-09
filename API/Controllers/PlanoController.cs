using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Planos;
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
    public class PlanoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlanoServico _planoServico;
        private readonly IHttpContextAccessor _httpContext;

        public PlanoController(
            IMapper mapper,
            IPlanoServico planoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _planoServico = planoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca plano a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o plano pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(PlanoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid planoID)
        {
            Plano? plano = await _planoServico.BuscarPorID(planoID);
            if (plano == null)
                return NotFound("Nenhum plano encontrado");


            var resultado = _mapper.Map<PlanoViewModel>(plano);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os planos pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<PlanoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var planos = await _planoServico.BuscarPorNome(parametro);

            if (planos == null || planos.Count == 0)
                return NotFound("Nenhum plano encontrado");

            var resultado = _mapper.Map<List<PlanoViewModel>>(planos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os planos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<PlanoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var planos = await _planoServico.BuscarTodos();

            if (planos == null || planos.Count == 0)
                return NotFound("Nenhum plano encontrado");


            var resultado = _mapper.Map<List<PlanoViewModel>>(planos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um plano
        /// </summary>
        /// <response code="202">Plano criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Plano atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PlanoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarPlanoInputModel body)
        {
            var (criou, planoId) = await _planoServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new PlanoIdResponseViewModel(planoId));

            return NoContent(); // Atualizado com sucesso, sem corpo 
        }
    }
}
