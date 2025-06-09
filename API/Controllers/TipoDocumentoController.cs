using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.TiposDocumentos;
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
    public class TipoDocumentoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoDocumentoServico _tipoDocumentoServico;
        private readonly IHttpContextAccessor _httpContext;

        public TipoDocumentoController(
            IMapper mapper,
            ITipoDocumentoServico tipoDocumentoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _tipoDocumentoServico = tipoDocumentoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de documento a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o documento pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(TipoDocumentoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int tipoDocumentoID)
        {
            TipoDocumento? tipoDocumento = await _tipoDocumentoServico.BuscarPorID(tipoDocumentoID);
            if (tipoDocumento == null)
                return NotFound("Nenhum tipo de documento encontrado");


            var resultado = _mapper.Map<TipoDocumentoViewModel>(tipoDocumento);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de documentos pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<TipoDocumentoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var tiposdocumentos = await _tipoDocumentoServico.BuscarPorNome(parametro);

            if (tiposdocumentos == null || tiposdocumentos.Count == 0)
                return NotFound("Nenhum tipo de documento encontrado");

            var resultado = _mapper.Map<List<TipoDocumentoViewModel>>(tiposdocumentos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de documentos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<TipoDocumentoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var tiposdocumentos = await _tipoDocumentoServico.BuscarTodos();

            if (tiposdocumentos == null || tiposdocumentos.Count == 0)
                return NotFound("Nenhum tipo de documento encontrado");


            var resultado = _mapper.Map<List<TipoDocumentoViewModel>>(tiposdocumentos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um tipo de documento
        /// </summary>
        /// <response code="202">Tipo de documento criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Tipo de documento atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoDocumentoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarTipoDocumentoInputModel body)
        {
            var (criou, tipoDocumentoId) = await _tipoDocumentoServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new TipoDocumentoIdResponseViewModel(tipoDocumentoId));

            return NoContent(); // Atualizado com sucesso, sem corpo 
        }
    }
}
