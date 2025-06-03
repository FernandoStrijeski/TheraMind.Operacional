using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.EmpresasAssinaturas;
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
    public class EmpresaAssinaturaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaAssinaturaServico _empresaAssinaturaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaAssinaturaController(
            IMapper mapper,
            IEmpresaAssinaturaServico empresaAssinaturaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaAssinaturaServico = empresaAssinaturaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a assinatura da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a assinatura da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(EmpresaAssinaturaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid empresaAssinaturaID)
        {
            EmpresaAssinatura? empresaAssinatura = await _empresaAssinaturaServico.BuscarPorID(empresaAssinaturaID);
            if (empresaAssinatura == null)
                return NotFound("Nenhuma assinatura de empresa encontrada");


            var resultado = _mapper.Map<EmpresaAssinaturaViewModel>(empresaAssinatura);
            return Ok(resultado);
        }



        /// <summary>
        /// Busca todas as assinaturas das empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<EmpresaAssinaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var empresasAssinaturas = await _empresaAssinaturaServico.BuscarTodos();

            if (empresasAssinaturas == null || empresasAssinaturas.Count == 0)
                return NotFound("Nenhuma assinatura de empresa encontrada");

            var resultado = _mapper.Map<List<EmpresaAssinaturaViewModel>>(empresasAssinaturas);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as assinaturas da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("BuscarPorIdEmpresa")]
        [ProducesResponseType(
            typeof(List<EmpresaAssinaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorIdEmpresa(Guid empresaID)
        {
            var empresasAssinaturas = await _empresaAssinaturaServico.BuscarPorIdEmpresa(empresaID);

            if (empresasAssinaturas == null || empresasAssinaturas.Count == 0)
                return NotFound("Nenhuma assinatura de empresa encontrada");

            var resultado = _mapper.Map<List<EmpresaAssinaturaViewModel>>(empresasAssinaturas);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma assinatura da empresa
        /// </summary>
        /// <response code="202">Assinatura da empresa criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Assinatura da empresa atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaAssinaturaIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarEmpresaAssinaturaInputModel body)
        {
            var (criou, empresaAssinaturaId) = await _empresaAssinaturaServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new EmpresaAssinaturaIdResponseViewModel(empresaAssinaturaId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo
        }
    }
}
