using API.Core.Filtros;
using API.modelos;
using API.Operacional.modelos.ViewModels;
using API.Servicos.OrientacoesSexuais;
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
    public class OrientacaoSexualController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrientacaoSexualServico _orientacaoSexualServico;
        private readonly IHttpContextAccessor _httpContext;

        public OrientacaoSexualController(
            IMapper mapper,
            IOrientacaoSexualServico orientacaoSexualServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _orientacaoSexualServico = orientacaoSexualServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de orientação sexual a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de orientação sexual pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(OrientacaoSexualViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int orientacaoSexualID)
        {
            OrientacaoSexual? orientacaoSexual = await _orientacaoSexualServico.BuscarPorID(orientacaoSexualID);
            if (orientacaoSexual == null)
                return NotFound("Nenhum tipo de orientação sexual encontrado");


            var resultado = _mapper.Map<OrientacaoSexualViewModel>(orientacaoSexual);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de orientações sexuais pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<OrientacaoSexualViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var orientacaoSexual = await _orientacaoSexualServico.BuscarPorNome(parametro);

            if (orientacaoSexual == null || orientacaoSexual.Count == 0)
                return NotFound("Nenhum tipo de orientação sexual encontrado");

            var resultado = _mapper.Map<List<OrientacaoSexualViewModel>>(orientacaoSexual);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de orientações sexuais
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<OrientacaoSexualViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var orientacaoSexual = await _orientacaoSexualServico.BuscarTodos();

            if (orientacaoSexual == null || orientacaoSexual.Count == 0)
                return NotFound("Nenhum tipo de orientação sexual encontrado");


            var resultado = _mapper.Map<List<OrientacaoSexualViewModel>>(orientacaoSexual);
            return Ok(resultado);
        }
    }
}
