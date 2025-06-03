using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.FormulariosSessoes;
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
    public class FormularioSessaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFormularioSessaoServico _formularioSessaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public FormularioSessaoController(
            IMapper mapper,
            IFormularioSessaoServico formularioSessaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _formularioSessaoServico = formularioSessaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o formulário de sessão a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o formulário de sessão pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(FormularioSessaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int formularioSessaoID)
        {
            FormularioSessao? formularioSessao = await _formularioSessaoServico.BuscarPorID(formularioSessaoID);
            if (formularioSessao == null)
                return NotFound("Nenhum formulário de sessão encontrado");


            var resultado = _mapper.Map<FormularioSessaoViewModel>(formularioSessao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os formulários de sessão pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var formularioSessoes = await _formularioSessaoServico.BuscarPorNome(parametro);

            if (formularioSessoes == null || formularioSessoes.Count == 0)
                return NotFound("Nenhum formulário de sessão encontrado");

            var resultado = _mapper.Map<List<FormularioSessaoViewModel>>(formularioSessoes);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os formulários de sessão
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var formularioSessoes = await _formularioSessaoServico.BuscarTodos();

            if (formularioSessoes == null || formularioSessoes.Count == 0)
                return NotFound("Nenhum formulário de sessão encontrado");


            var resultado = _mapper.Map<List<FormularioSessaoViewModel>>(formularioSessoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um formulário de sessão
        /// </summary>
        /// <response code="202">Formulário de sessão criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Formulário de sessão atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FormularioSessaoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarFormularioSessaoInputModel body)
        {
            var (criou, formularioSessaoId) = await _formularioSessaoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new FormularioSessaoIdResponseViewModel(formularioSessaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
