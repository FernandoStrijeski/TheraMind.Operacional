using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.Servicos.TiposLogradouros;
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
    public class TipoLogradouroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoLogradouroServico _tipoLogradouroServico;
        private readonly IHttpContextAccessor _httpContext;

        public TipoLogradouroController(
            IMapper mapper,
            ITipoLogradouroServico tipoLogradouroServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _tipoLogradouroServico = tipoLogradouroServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de logradouro a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o logradouro pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(TipoLogradouroViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string tipoLogradouroID)
        {
            TipoLogradouro? tipoLogradouro = await _tipoLogradouroServico.BuscarPorID(tipoLogradouroID);
            if (tipoLogradouro == null)
                return NotFound("Nenhum tipo de logradouro encontrado");


            var resultado = _mapper.Map<TipoLogradouroViewModel>(tipoLogradouro);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de logradouros pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<TipoLogradouroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var tiposLogradouros = await _tipoLogradouroServico.BuscarPorNome(parametro);

            if (tiposLogradouros == null || tiposLogradouros.Count == 0)
                return NotFound("Nenhum tipo de logradouro encontrado");

            var resultado = _mapper.Map<List<TipoLogradouroViewModel>>(tiposLogradouros);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de logradouros pelo nome
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<TipoLogradouroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var tiposLogradouros = await _tipoLogradouroServico.BuscarTodos();

            if (tiposLogradouros == null || tiposLogradouros.Count == 0)
                return NotFound("Nenhum tipo de logradouro encontrado");


            var resultado = _mapper.Map<List<TipoLogradouroViewModel>>(tiposLogradouros);
            return Ok(resultado);
        }
    }
}
