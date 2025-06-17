using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.TiposEtnias;
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
    public class TipoEtniaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoEtniaServico _tipoEtniaServico;
        private readonly IHttpContextAccessor _httpContext;

        public TipoEtniaController(
            IMapper mapper,
            ITipoEtniaServico tipoEtniaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _tipoEtniaServico = tipoEtniaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de etnia a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a etnia pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(TipoEtniaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int tipoEtniaID)
        {
            TipoEtnia? tipoEtnia = await _tipoEtniaServico.BuscarPorID(tipoEtniaID);
            if (tipoEtnia == null)
                return NotFound("Nenhum tipo de etina encontrado");


            var resultado = _mapper.Map<TipoEtniaViewModel>(tipoEtnia);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de etnias pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<TipoEtniaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var tiposEtnias = await _tipoEtniaServico.BuscarPorNome(parametro);

            if (tiposEtnias == null || tiposEtnias.Count == 0)
                return NotFound("Nenhum tipo de etnia encontrado");

            var resultado = _mapper.Map<List<TipoEtniaViewModel>>(tiposEtnias);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de etnias
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<TipoEtniaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var tiposEtnias = await _tipoEtniaServico.BuscarTodos();

            if (tiposEtnias == null || tiposEtnias.Count == 0)
                return NotFound("Nenhum tipo de etnia encontrado");


            var resultado = _mapper.Map<List<TipoEtniaViewModel>>(tiposEtnias);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um tipo de etnia.
        /// </summary>         
        ///<response code="201">Tipo de etnia criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoEtniaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarTipoEtniaInputModel tipoEtnia)
        {
            var retorno = await _tipoEtniaServico.Adicionar(_mapper.Map<TipoEtnia>(tipoEtnia));
            return Ok(_mapper.Map<TipoEtniaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um tipo de etnia.
        /// </summary>         
        ///<response code="200">Tipo de etnia atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoEtniaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] TipoEtniaInputModel tipoEtnia)
        {
            // Busca o registro existente
            var tipoEtniaViewModelExistente = await _tipoEtniaServico.BuscarPorID(tipoEtnia.TipoEtniaId);
            if (tipoEtniaViewModelExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(tipoEtnia, tipoEtniaViewModelExistente); // Faz o merge

            var retorno = await _tipoEtniaServico.Atualizar(_mapper.Map<TipoEtnia>(tipoEtniaViewModelExistente));
            return Ok(_mapper.Map<TipoEtniaViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um tipo de etnia.
        /// </summary>         
        ///<response code="200">Tipo de etnia excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int tipoEtniaID)
        {
            await _tipoEtniaServico.Deletar(tipoEtniaID);
            return Ok();
        }
    }
}
