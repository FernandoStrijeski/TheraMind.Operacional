using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Empresas;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(1.0)]
    [RequerValidacaoDeToken]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaServico _empresaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaController(
            IMapper mapper,
            IEmpresaServico empresaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaServico = empresaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(Guid empresaID)
        {
            Empresa? empresa = await _empresaServico.BuscarPorID(empresaID);
            if (empresa == null)
                return NotFound("Nenhum tipo de etina encontrado");


            var resultado = _mapper.Map<EmpresaViewModel>(empresa);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma empresa
        /// </summary>
        /// <response code="202">Empresa criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Empresa atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarEmpresaInputModel body)
        {
            var (criou, empresaId) = await _empresaServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new EmpresaIdResponseViewModel(empresaId));

            return NoContent(); // Atualizado com sucesso, sem corpo    
        }
    }
}
