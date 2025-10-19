using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.TiposDocumentos;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// Cria um tipo de documento.
        /// </summary>         
        ///<response code="201">Tipo de documento criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoDocumentoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarTipoDocumentoInputModel tipoDocumento)
        {
            var retorno = await _tipoDocumentoServico.Adicionar(_mapper.Map<TipoDocumento>(tipoDocumento));
            return Ok(_mapper.Map<TipoDocumentoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um tipo de documento.
        /// </summary>         
        ///<response code="200">Tipo de documento atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoDocumentoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] TipoDocumentoInputModel tipoDocumento)
        {
            // Busca o registro existente
            var tipoDocumentoExistente = await _tipoDocumentoServico.BuscarPorID(tipoDocumento.TipoDocumentoId);
            if (tipoDocumentoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(tipoDocumento, tipoDocumentoExistente); // Faz o merge

            var retorno = await _tipoDocumentoServico.Atualizar(_mapper.Map<TipoDocumento>(tipoDocumentoExistente));
            return Ok(_mapper.Map<TipoDocumentoViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um tipo de documento.
        /// </summary>         
        ///<response code="200">Tipo de documento excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int tipoDocumentoID)
        {
            await _tipoDocumentoServico.Deletar(tipoDocumentoID);
            return Ok();
        }
    }
}
