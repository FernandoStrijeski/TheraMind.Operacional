using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
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
    public class ConvenioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConvenioServico _convenioServico;
        private readonly IHttpContextAccessor _httpContext;

        public ConvenioController(
            IMapper mapper,
            IConvenioServico convenioServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _convenioServico = convenioServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o convênio a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o convênio pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ConvenioViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int convenioID)
        {
            Convenio? convenio = await _convenioServico.BuscarPorID(convenioID);
            if (convenio == null)
                return NotFound("Nenhum convênio encontrado");


            var resultado = _mapper.Map<ConvenioViewModel>(convenio);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os convênios pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<ConvenioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var convenios = await _convenioServico.BuscarPorNome(parametro);

            if (convenios == null || convenios.Count == 0)
                return NotFound("Nenhum convênio encontrado");

            var resultado = _mapper.Map<List<ConvenioViewModel>>(convenios);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os convênios
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ConvenioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var convenios = await _convenioServico.BuscarTodos();

            if (convenios == null || convenios.Count == 0)
                return NotFound("Nenhum convênio encontrado");


            var resultado = _mapper.Map<List<ConvenioViewModel>>(convenios);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um convênio
        /// </summary>
        /// <response code="202">Convênio criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Convênio atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ConvenioIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarConvenioInputModel body)
        {
            var (criou, convenioId) = await _convenioServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ConvenioIdResponseViewModel(convenioId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
