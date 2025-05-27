using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.EmpresaFaturas;
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
    public class EmpresaFaturaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaFaturaServico _empresaFaturaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaFaturaController(
            IMapper mapper,
            IEmpresaFaturaServico empresaFaturaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaFaturaServico = empresaFaturaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a fatura da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a fatura da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(EmpresaFaturaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int empresaFaturaID)
        {
            EmpresaFatura? empresaFatura = await _empresaFaturaServico.BuscarPorID(empresaFaturaID);
            if (empresaFatura == null)
                return NotFound("Nenhuma fatura da empresa encontrada");


            var resultado = _mapper.Map<EmpresaFaturaViewModel>(empresaFatura);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as faturas da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<EmpresaFaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var empresaFaturas = await _empresaFaturaServico.BuscarTodos();

            if (empresaFaturas == null || empresaFaturas.Count == 0)
                return NotFound("Nenhuma fatura da empresa encontrada");


            var resultado = _mapper.Map<List<EmpresaFaturaViewModel>>(empresaFaturas);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma fatura da empresa
        /// </summary>
        /// <response code="202">Fatura da empresa criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Fatura da empresa atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaFaturaIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarEmpresaFaturaInputModel body)
        {
            var (criou, empresaFaturaId) = await _empresaFaturaServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new EmpresaFaturaIdResponseViewModel(empresaFaturaId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
