using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.Servicos.TiposEtnias;
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
    public class TipoEtniaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoEtniaServico _tipoEtniaServico;
        private readonly IHttpContextAccessor _httpContext;

        public TipoEtniaController(
            IMapper mapper,
            ITipoEtniaServico tipoEtniaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _tipoEtniaServico = tipoEtniaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de etnia a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a etnia pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(TipoEtniaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int tipoEtniaID)
        {
            TipoEtnia? tipoEtnia = await _tipoEtniaServico.BuscarPorID(tipoEtniaID);
            if (tipoEtnia == null)
                return NotFound("Nenhum tipo de etina encontrado");


            var resultado = _mapper.Map<TipoEtniaViewModel>(tipoEtnia);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de etnias pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<TipoEtniaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var tiposEtnias = await _tipoEtniaServico.BuscarPorNome(parametro);

            if (tiposEtnias == null || tiposEtnias.Count == 0)
                return NotFound("Nenhum tipo de etnia encontrado");

            var resultado = _mapper.Map<List<TipoEtniaViewModel>>(tiposEtnias);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de etnias
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<TipoEtniaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var tiposEtnias = await _tipoEtniaServico.BuscarTodos();

            if (tiposEtnias == null || tiposEtnias.Count == 0)
                return NotFound("Nenhum tipo de etnia encontrado");


            var resultado = _mapper.Map<List<TipoEtniaViewModel>>(tiposEtnias);
            return Ok(resultado);
        }
    }
}
