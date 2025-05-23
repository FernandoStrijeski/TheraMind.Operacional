using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.DocumentosModelos;
using API.Servicos.DocumentosModelosEmpresas;
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
    public class DocumentoModeloEmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentoModeloEmpresaServico _documentoModeloEmpresaServico;
        private readonly IHttpContextAccessor _httpContext;

        public DocumentoModeloEmpresaController(
            IMapper mapper,
            IDocumentoModeloEmpresaServico documentoModeloEmpresaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _documentoModeloEmpresaServico = documentoModeloEmpresaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o modelo de documento da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o modelo de documento da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(DocumentoModeloEmpresaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int documentoModeloEmpresaID)
        {
            DocumentoModeloEmpresa? documentoModeloEmpresa = await _documentoModeloEmpresaServico.BuscarPorID(documentoModeloEmpresaID);
            if (documentoModeloEmpresa == null)
                return NotFound("Nenhum modelo de documento da empresa encontrado");


            var resultado = _mapper.Map<DocumentoModeloEmpresaViewModel>(documentoModeloEmpresa);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os modelos de documentos da empresa pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<DocumentoModeloEmpresaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var documentosModelosEmpresas = await _documentoModeloEmpresaServico.BuscarPorNome(parametro);

            if (documentosModelosEmpresas == null || documentosModelosEmpresas.Count == 0)
                return NotFound("Nenhum modelo de documento da empresa encontrado");

            var resultado = _mapper.Map<List<DocumentoModeloEmpresaViewModel>>(documentosModelosEmpresas);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os modelos de documentos da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<DocumentoModeloEmpresaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var documentosModelosEmpresas = await _documentoModeloEmpresaServico.BuscarTodos();

            if (documentosModelosEmpresas == null || documentosModelosEmpresas.Count == 0)
                return NotFound("Nenhum modelo de documento da empresa encontrado");


            var resultado = _mapper.Map<List<DocumentoModeloEmpresaViewModel>>(documentosModelosEmpresas);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um modelo de documento da empresa
        /// </summary>
        /// <response code="202">Modelo de documento da empresa criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Modelo de documento da empresa atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarDocumentoModeloEmpresaInputModel body)
        {
            var (criou, documentoModeloEmpresaId) = await _documentoModeloEmpresaServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new DocumentoModeloEmpresaIdResponseViewModel(documentoModeloEmpresaId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
