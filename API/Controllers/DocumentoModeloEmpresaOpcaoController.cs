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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria uma opção para modelo de documento da empresa.
        /// </summary>         
        ///<response code="201">Opção para modelo de documento da empresa criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaOpcaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarDocumentoModeloEmpresaOpcaoInputModel documentoModeloEmpresaOpcao)
        {
            var retorno = await _documentoModeloEmpresaOpcaoServico.Adicionar(_mapper.Map<DocumentoModeloEmpresaOpcao>(documentoModeloEmpresaOpcao));
            return Ok(_mapper.Map<DocumentoModeloEmpresaOpcaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma opção para modelo de documento da empresa.
        /// </summary>         
        ///<response code="200">Opção para modelo de documento da empresa atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaOpcaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] DocumentoModeloEmpresaOpcaoInputModel documentoModeloEmpresaOpcao)
        {
            // Busca o registro existente
            var documentoModeloEmpresaOpcaoExistente = await _documentoModeloEmpresaOpcaoServico.BuscarPorID(documentoModeloEmpresaOpcao.DocumentoModeloEmpresaOpcaoId);
            if (documentoModeloEmpresaOpcaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(documentoModeloEmpresaOpcao, documentoModeloEmpresaOpcaoExistente); // Faz o merge

            var retorno = await _documentoModeloEmpresaOpcaoServico.Atualizar(_mapper.Map<DocumentoModeloEmpresaOpcao>(documentoModeloEmpresaOpcaoExistente));
            return Ok(_mapper.Map<DocumentoModeloEmpresaOpcaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma opção para modelo de documento da empresa.
        /// </summary>         
        ///<response code="200">Opção para modelo de documento da empresa excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int documentoModeloEmpresaOpcaoID)
        {
            await _documentoModeloEmpresaOpcaoServico.Deletar(documentoModeloEmpresaOpcaoID);
            return Ok();
        }
    }
}
