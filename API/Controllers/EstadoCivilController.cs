using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.Servicos.EstadosCivis;
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
    public class EstadoCivilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEstadoCivilServico _estadoCivilServico;
        private readonly IHttpContextAccessor _httpContext;

        public EstadoCivilController(
            IMapper mapper,
            IEstadoCivilServico estadoCivilServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _estadoCivilServico = estadoCivilServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de estado civil a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de estado civil pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(EstadoCivilViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string estadoCivilID)
        {
            EstadoCivil? estadoCivil = await _estadoCivilServico.BuscarPorID(estadoCivilID);
            if (estadoCivil == null)
                return NotFound("Nenhum tipo de estado civil encontrado");


            var resultado = _mapper.Map<EstadoCivilViewModel>(estadoCivil);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de estados civis pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<EstadoCivilViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var estadoCivil = await _estadoCivilServico.BuscarPorNome(parametro);

            if (estadoCivil == null || estadoCivil.Count == 0)
                return NotFound("Nenhum tipo de estado civil encontrado");

            var resultado = _mapper.Map<List<EstadoCivilViewModel>>(estadoCivil);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de estados civis pelo nome
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<EstadoCivilViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var estadoCivil = await _estadoCivilServico.BuscarTodos();

            if (estadoCivil == null || estadoCivil.Count == 0)
                return NotFound("Nenhum tipo de estado civil encontrado");


            var resultado = _mapper.Map<List<EstadoCivilViewModel>>(estadoCivil);
            return Ok(resultado);
        }
    }
}
