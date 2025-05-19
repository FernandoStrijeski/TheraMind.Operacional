using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.Servicos.Nacionalidades;
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
    public class NacionalidadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INacionalidadeServico _nacionalidadeServico;
        private readonly IHttpContextAccessor _httpContext;

        public NacionalidadeController(
            IMapper mapper,
            INacionalidadeServico nacionalidadeServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _nacionalidadeServico = nacionalidadeServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a nacionalidade a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a nacionalidade pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(NacionalidadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int nacionalidadeID)
        {
            Nacionalidade? nacionalidade = await _nacionalidadeServico.BuscarPorID(nacionalidadeID);
            if (nacionalidade == null)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<NacionalidadeViewModel>(nacionalidade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as nacionalidades pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<NacionalidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var nacionalidade = await _nacionalidadeServico.BuscarPorNome(parametro);

            if (nacionalidade == null || nacionalidade.Count == 0)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<List<NacionalidadeViewModel>>(nacionalidade);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as nacionalidade
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<NacionalidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var nacionalidade = await _nacionalidadeServico.BuscarTodos();

            if (nacionalidade == null || nacionalidade.Count == 0)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<List<NacionalidadeViewModel>>(nacionalidade);
            return Ok(resultado);
        }
    }
}
