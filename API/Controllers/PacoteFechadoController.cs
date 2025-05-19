using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Convenios;
using API.Servicos.PacotesFechados;
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
    public class PacoteFechadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPacoteFechadoServico _pacoteFechadoServico;
        private readonly IHttpContextAccessor _httpContext;

        public PacoteFechadoController(
            IMapper mapper,
            IPacoteFechadoServico pacoteFechadoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _pacoteFechadoServico = pacoteFechadoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o pacote fechado a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o pacote fechado pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(PacoteFechadoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int pacoteFechadoID)
        {
            PacoteFechado? pacoteFechado = await _pacoteFechadoServico.BuscarPorID(pacoteFechadoID);
            if (pacoteFechado == null)
                return NotFound("Nenhum pacote fechado encontrado");


            var resultado = _mapper.Map<PacoteFechadoViewModel>(pacoteFechado);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os pacotes fechados
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<PacoteFechadoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var pacotesFechados = await _pacoteFechadoServico.BuscarTodos();

            if (pacotesFechados == null || pacotesFechados.Count == 0)
                return NotFound("Nenhum pacote fechado encontrado");


            var resultado = _mapper.Map<List<PacoteFechadoViewModel>>(pacotesFechados);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um pacote fechado
        /// </summary>
        /// <response code="202">Pacote fechado criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Pacote fechado atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PacoteFechadoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarPacoteFechadoInputModel body)
        {
            var (criou, pacoteFechadoId) = await _pacoteFechadoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new PacoteFechadoIdResponseViewModel(pacoteFechadoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
