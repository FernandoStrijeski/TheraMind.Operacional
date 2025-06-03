using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.DocumentosModelosEmpresasOpcoes;
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
    public class DocumentoModeloEmpresaOpcaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDocumentoModeloEmpresaOpcaoServico _documentoModeloEmpresaOpcaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public DocumentoModeloEmpresaOpcaoController(
            IMapper mapper,
            IDocumentoModeloEmpresaOpcaoServico documentoModeloEmpresaOpcaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _documentoModeloEmpresaOpcaoServico = documentoModeloEmpresaOpcaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a opção para modelo de documento da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a opção para modelo de documento da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(DocumentoModeloEmpresaOpcaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int documentoModeloEmpresaOpcaoID)
        {
            DocumentoModeloEmpresaOpcao? documentoModeloEmpresaOpcao = await _documentoModeloEmpresaOpcaoServico.BuscarPorID(documentoModeloEmpresaOpcaoID);
            if (documentoModeloEmpresaOpcao == null)
                return NotFound("Nenhuma opção para modelo de documento da empresa encontrado");


            var resultado = _mapper.Map<DocumentoModeloEmpresaOpcaoViewModel>(documentoModeloEmpresaOpcao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as opções para os modelos de documentos da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<DocumentoModeloEmpresaOpcaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var documentosModelosEmpresasOpcoes = await _documentoModeloEmpresaOpcaoServico.BuscarTodos();

            if (documentosModelosEmpresasOpcoes == null || documentosModelosEmpresasOpcoes.Count == 0)
                return NotFound("Nenhuma opção para modelo de documento da empresa encontrado");


            var resultado = _mapper.Map<List<DocumentoModeloEmpresaOpcaoViewModel>>(documentosModelosEmpresasOpcoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma opção para uso nos modelos de documentos da empresa
        /// </summary>
        /// <response code="202">Opção para modelo de documento da empresa criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Õpção para modelo de documento da empresa atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaOpcaoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarDocumentoModeloEmpresaOpcaoInputModel body)
        {
            var (criou, documentoModeloEmpresaOpcaoId) = await _documentoModeloEmpresaOpcaoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new DocumentoModeloEmpresaOpcaoIdResponseViewModel(documentoModeloEmpresaOpcaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
