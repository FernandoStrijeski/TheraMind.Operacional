using API.Core.Filtros;
using API.modelos;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Escolaridades;
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
    public class EscolaridadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEscolaridadeServico _escolaridadeServico;
        private readonly IHttpContextAccessor _httpContext;

        public EscolaridadeController(
            IMapper mapper,
            IEscolaridadeServico escolaridadeServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _escolaridadeServico = escolaridadeServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de escolaridade a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de escolaridade pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EscolaridadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int escolaridadeID)
        {
            Escolaridade? escolaridade = await _escolaridadeServico.BuscarPorID(escolaridadeID);
            if (escolaridade == null)
                return NotFound("Nenhum tipo de escolaridade encontrado");


            var resultado = _mapper.Map<EscolaridadeViewModel>(escolaridade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de escolaridades pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<EscolaridadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var escolaridade = await _escolaridadeServico.BuscarPorNome(parametro);

            if (escolaridade == null || escolaridade.Count == 0)
                return NotFound("Nenhum tipo de escolaridade encontrado");

            var resultado = _mapper.Map<List<EscolaridadeViewModel>>(escolaridade);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de escolaridades
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EscolaridadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var escolaridade = await _escolaridadeServico.BuscarTodos();

            if (escolaridade == null || escolaridade.Count == 0)
                return NotFound("Nenhum tipo de escolaridade encontrado");


            var resultado = _mapper.Map<List<EscolaridadeViewModel>>(escolaridade);
            return Ok(resultado);
        }
    }
}
