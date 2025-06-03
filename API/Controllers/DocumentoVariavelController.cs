using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.DocumentosVariaveis;
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
    public class DocumentoVariavelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentoVariavelServico _documentoVariavelServico;
        private readonly IHttpContextAccessor _httpContext;

        public DocumentoVariavelController(
            IMapper mapper,
            IDocumentoVariavelServico documentoVariavelServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _documentoVariavelServico = documentoVariavelServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a variável do documento a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a variável do documento pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(DocumentoVariavelViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int documentoVariavelID)
        {
            DocumentoVariavel? documentoVariavel = await _documentoVariavelServico.BuscarPorID(documentoVariavelID);
            if (documentoVariavel == null)
                return NotFound("Nenhuma variável de documento encontrada");


            var resultado = _mapper.Map<DocumentoVariavelViewModel>(documentoVariavel);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as variáveis do documento pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<DocumentoVariavelViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var documentosVariaveis = await _documentoVariavelServico.BuscarPorNome(parametro);

            if (documentosVariaveis == null || documentosVariaveis.Count == 0)
                return NotFound("Nenhuma variável do documento encontrada");

            var resultado = _mapper.Map<List<DocumentoVariavelViewModel>>(documentosVariaveis);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as variáveis de documento
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<DocumentoVariavelViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var documentosVariaveis = await _documentoVariavelServico.BuscarTodos();

            if (documentosVariaveis == null || documentosVariaveis.Count == 0)
                return NotFound("Nenhuma variável de documento encontrada");


            var resultado = _mapper.Map<List<DocumentoVariavelViewModel>>(documentosVariaveis);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma variável de documento
        /// </summary>
        /// <response code="202">Variável do documento criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Variável do documento atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoVariavelIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarDocumentoVariavelInputModel body)
        {
            var (criou, documentoVariavelId) = await _documentoVariavelServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new DocumentoVariavelIdResponseViewModel(documentoVariavelId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
