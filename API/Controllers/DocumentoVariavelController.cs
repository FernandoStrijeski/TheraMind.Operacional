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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorNome")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria uma variável de documento.
        /// </summary>         
        ///<response code="201">Variável de documento criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoVariavelViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarDocumentoVariavelInputModel documentoVariavel)
        {
            var retorno = await _documentoVariavelServico.Adicionar(_mapper.Map<DocumentoVariavel>(documentoVariavel));
            return Ok(_mapper.Map<DocumentoVariavelViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma variável de documento.
        /// </summary>         
        ///<response code="200">Variável de documento atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoVariavelViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] DocumentoVariavelInputModel documentoVariavel)
        {
            // Busca o registro existente
            var documentoVariavelExistente = await _documentoVariavelServico.BuscarPorID(documentoVariavel.DocumentoVariavelId);
            if (documentoVariavelExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(documentoVariavel, documentoVariavelExistente); // Faz o merge

            var retorno = await _documentoVariavelServico.Atualizar(_mapper.Map<DocumentoVariavel>(documentoVariavelExistente));
            return Ok(_mapper.Map<DocumentoVariavelInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma variável de documento.
        /// </summary>         
        ///<response code="200">Variável de documento excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int documentoVariavelID)
        {
            await _documentoVariavelServico.Deletar(documentoVariavelID);
            return Ok();
        }
    }
}
