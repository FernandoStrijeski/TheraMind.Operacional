using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.Servicos.Estados;
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
    public class EstadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEstadoServico _estadoServico;
        private readonly IHttpContextAccessor _httpContext;

        public EstadoController(
            IMapper mapper,
            IEstadoServico estadoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _estadoServico = estadoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o estado a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o estado pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(EstadoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string estadoID)
        {
            Estado? estado = await _estadoServico.BuscarPorID(estadoID);
            if (estado == null)
                return NotFound("Nenhum estado encontrado");


            var resultado = _mapper.Map<EstadoViewModel>(estado);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os estados pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<EstadoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var estado = await _estadoServico.BuscarPorNome(parametro);

            if (estado == null || estado.Count == 0)
                return NotFound("Nenhum estado encontrado");

            var resultado = _mapper.Map<List<EstadoViewModel>>(estado);
            return Ok(resultado);
        }
    }
}
