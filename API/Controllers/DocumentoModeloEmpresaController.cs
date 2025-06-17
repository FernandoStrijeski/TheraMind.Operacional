using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorNome")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria um modelo de documento da empresa.
        /// </summary>         
        ///<response code="201">Modelo de documento da empresa criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarDocumentoModeloEmpresaInputModel documentoModeloEmpresa)
        {
            var retorno = await _documentoModeloEmpresaServico.Adicionar(_mapper.Map<DocumentoModeloEmpresa>(documentoModeloEmpresa));
            return Ok(_mapper.Map<DocumentoModeloEmpresaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um modelo de documento da empresa.
        /// </summary>         
        ///<response code="200">Modelo de documento da empresa atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloEmpresaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] DocumentoModeloEmpresaInputModel documentoModeloEmpresa)
        {
            // Busca o registro existente
            var documentoModeloEmpresaExistente = await _documentoModeloEmpresaServico.BuscarPorID(documentoModeloEmpresa.DocumentoModeloEmpresaId);
            if (documentoModeloEmpresaExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(documentoModeloEmpresa, documentoModeloEmpresaExistente); // Faz o merge

            var retorno = await _documentoModeloEmpresaServico.Atualizar(_mapper.Map<DocumentoModeloEmpresa>(documentoModeloEmpresaExistente));
            return Ok(_mapper.Map<DocumentoModeloEmpresaInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um modelo de documento da empresa.
        /// </summary>         
        ///<response code="200">Modelo de documento da empresa excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int documentoModeloEmpresaID)
        {
            await _documentoModeloEmpresaServico.Deletar(documentoModeloEmpresaID);
            return Ok();
        }
    }
}
