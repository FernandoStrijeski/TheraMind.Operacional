using API.Core.Filtros;
using API.modelos;
using API.Operacional.modelos.ViewModels;
using API.Servicos.IdentidadesGeneros;
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
    public class IdentidadeGeneroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentidadeGeneroServico _identidadeGeneroServico;
        private readonly IHttpContextAccessor _httpContext;

        public IdentidadeGeneroController(
            IMapper mapper,
            IIdentidadeGeneroServico identidadeGeneroServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _identidadeGeneroServico = identidadeGeneroServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de identidade de gênero a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de identidade de gênero pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(IdentidadeGeneroViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int identidadeGeneroID)
        {
            IdentidadeGenero? identidadeGenero = await _identidadeGeneroServico.BuscarPorID(identidadeGeneroID);
            if (identidadeGenero == null)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");


            var resultado = _mapper.Map<IdentidadeGeneroViewModel>(identidadeGenero);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de identidades de gêneros pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<IdentidadeGeneroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var identidadeGenero = await _identidadeGeneroServico.BuscarPorNome(parametro);

            if (identidadeGenero == null || identidadeGenero.Count == 0)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");

            var resultado = _mapper.Map<List<IdentidadeGeneroViewModel>>(identidadeGenero);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de identidades de gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<IdentidadeGeneroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var identidadeGenero = await _identidadeGeneroServico.BuscarTodos();

            if (identidadeGenero == null || identidadeGenero.Count == 0)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");


            var resultado = _mapper.Map<List<IdentidadeGeneroViewModel>>(identidadeGenero);
            return Ok(resultado);
        }
    }
}
