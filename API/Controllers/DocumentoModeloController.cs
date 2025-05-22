using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.DocumentosModelos;
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
    public class DocumentoModeloController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentoModeloServico _documentoModeloServico;
        private readonly IHttpContextAccessor _httpContext;

        public DocumentoModeloController(
            IMapper mapper,
            IDocumentoModeloServico documentoModeloServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _documentoModeloServico = documentoModeloServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o modelo de documento a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o modelo de documento pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(DocumentoModeloViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int documentoModeloID)
        {
            DocumentoModelo? documentoModelo = await _documentoModeloServico.BuscarPorID(documentoModeloID);
            if (documentoModelo == null)
                return NotFound("Nenhum modelo de documento encontrado");


            var resultado = _mapper.Map<DocumentoModeloViewModel>(documentoModelo);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os modelos de documentos pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<DocumentoModeloViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var documentosModelos = await _documentoModeloServico.BuscarPorNome(parametro);

            if (documentosModelos == null || documentosModelos.Count == 0)
                return NotFound("Nenhum modelo de documento encontrado");

            var resultado = _mapper.Map<List<DocumentoModeloViewModel>>(documentosModelos);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os modelos de documentos
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<DocumentoModeloViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var documentosModelos = await _documentoModeloServico.BuscarTodos();

            if (documentosModelos == null || documentosModelos.Count == 0)
                return NotFound("Nenhum modelo de documento encontrado");


            var resultado = _mapper.Map<List<DocumentoModeloViewModel>>(documentosModelos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um modelo de documento
        /// </summary>
        /// <response code="202">Modelo de documento criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Modelo de documento atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarDocumentoModeloInputModel body)
        {
            var (criou, documentoModeloId) = await _documentoModeloServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new DocumentoModeloIdResponseViewModel(documentoModeloId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
