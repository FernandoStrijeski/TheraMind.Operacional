using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Empresas;
using API.Servicos.Filiais;
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
    public class FilialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFilialServico _filialServico;
        private readonly IHttpContextAccessor _httpContext;

        public FilialController(
            IMapper mapper,
            IFilialServico filialServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _filialServico = filialServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a filial a partir dos identificadores informados
        /// </summary>
        /// <response code="200">Retorna a filial pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(Guid empresaID, int filialID)
        {
            Filial? filial = await _filialServico.BuscarPorID(empresaID, filialID);
            if (filial == null)
                return NotFound("Nenhuma filial encontrada"); ;

            return Ok(filial);
        }

        /// <summary>
        /// Cria ou atualiza uma filial
        /// </summary>
        /// <response code="202">Filial criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Empresa atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FilialIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarFilialInputModel body)
        {
            var (criou, filialId) = await _filialServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new FilialIdResponseViewModel(filialId));

            return NoContent(); // Atualizado com sucesso, sem corpo 

        }
    }
}
