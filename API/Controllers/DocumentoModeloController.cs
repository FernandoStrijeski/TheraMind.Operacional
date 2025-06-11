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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorNome")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria um modelo de documento.
        /// </summary>         
        ///<response code="201">Modelo de documento criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarDocumentoModeloInputModel documentoModelo)
        {
            var retorno = await _documentoModeloServico.Adicionar(_mapper.Map<DocumentoModelo>(documentoModelo));
            return Ok(_mapper.Map<DocumentoModeloViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um modelo de documento.
        /// </summary>         
        ///<response code="200">Modelo de documento atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(DocumentoModeloViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] DocumentoModeloInputModel documentoModelo)
        {
            // Busca o registro existente
            var documentoModeloExistente = await _documentoModeloServico.BuscarPorID(documentoModelo.DocumentoModeloId);
            if (documentoModeloExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(documentoModelo, documentoModeloExistente); // Faz o merge

            var retorno = await _documentoModeloServico.Atualizar(_mapper.Map<DocumentoModelo>(documentoModeloExistente));
            return Ok(_mapper.Map<DocumentoModeloInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um modelo de documento.
        /// </summary>         
        ///<response code="200">Modelo de documento excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _documentoModeloServico.Deletar(id);
            return Ok();
        }
    }
}
