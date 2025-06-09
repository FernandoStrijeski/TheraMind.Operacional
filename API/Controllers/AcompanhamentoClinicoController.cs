using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AcompanhamentosClinicos;
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
    public class AcompanhamentoClinicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAcompanhamentoClinicoServico _acompanhamentoClinicoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AcompanhamentoClinicoController(
            IMapper mapper,
            IAcompanhamentoClinicoServico acompanhamentoClinicoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _acompanhamentoClinicoServico = acompanhamentoClinicoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o acompanhamento clínico a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o acompanhamento clínico pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AcompanhamentoClinicoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid acompanhamentoClinicoID)
        {
            AcompanhamentoClinico? convenio = await _acompanhamentoClinicoServico.BuscarPorID(acompanhamentoClinicoID);
            if (convenio == null)
                return NotFound("Nenhum acompanhamento clínico encontrado");

            var resultado = _mapper.Map<AcompanhamentoClinicoViewModel>(convenio);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acompanhamentos clínicos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AcompanhamentoClinicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var acompanhamentosClinicos = await _acompanhamentoClinicoServico.BuscarTodos();

            if (acompanhamentosClinicos == null || acompanhamentosClinicos.Count == 0)
                return NotFound("Nenhum acompanhamento clínico encontrado");


            var resultado = _mapper.Map<List<AcompanhamentoClinicoViewModel>>(acompanhamentosClinicos);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acompanhamentos clínicos do Clientes e Profissional
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodosPorProfissionalCliente")]
        [ProducesResponseType(
            typeof(List<AcompanhamentoClinicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,PROFISSIONAL")]
        public async Task<ActionResult> BuscarTodosPorProfissionalCliente(Guid profissionalID, Guid clienteID)
        {
            var acompanhamentosClinicos = await _acompanhamentoClinicoServico.BuscarTodosPorProfissionalCliente(profissionalID, clienteID);

            if (acompanhamentosClinicos == null || acompanhamentosClinicos.Count == 0)
                return NotFound("Nenhum acompanhamento clínico encontrado");

            var resultado = _mapper.Map<List<AcompanhamentoClinicoViewModel>>(acompanhamentosClinicos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um acompanhamento clínico
        /// </summary>
        /// <response code="202">Acompanhamento clínico criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Acompanhamento clínico atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AcompanhamentoClinicoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAcompanhamentoClinicoInputModel body)
        {
            var (criou, acompanhamentoClinicoId) = await _acompanhamentoClinicoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AcompanhamentoClinicoIdResponseViewModel(acompanhamentoClinicoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
