using API.Core.Filtros;
using API.modelos;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Paises;
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
    public class PaisController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaisServico _paisServico;
        private readonly IHttpContextAccessor _httpContext;

        public PaisController(
            IMapper mapper,
            IPaisServico paisServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _paisServico = paisServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o país a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o país pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(PaisViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int paisID)
        {
            Pais? pais = await _paisServico.BuscarPorID(paisID);
            if (pais == null)
                return NotFound("Nenhum país encontrado");


            var resultado = _mapper.Map<PaisViewModel>(pais);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os países pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<PaisViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var pais = await _paisServico.BuscarPorNome(parametro);

            if (pais == null || pais.Count == 0)
                return NotFound("Nenhum país encontrado");

            var resultado = _mapper.Map<List<PaisViewModel>>(pais);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os países
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<PaisViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var paises = await _paisServico.BuscarTodos();

            if (paises == null || paises.Count == 0)
                return NotFound("Nenhum país encontrado");


            var resultado = _mapper.Map<List<PaisViewModel>>(paises);
            return Ok(resultado);
        }
    }
}
