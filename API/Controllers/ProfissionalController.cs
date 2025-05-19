using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Profissionais;
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
    public class ProfissionalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfissionalServico _profissionalServico;
        private readonly IHttpContextAccessor _httpContext;

        public ProfissionalController(
            IMapper mapper,
            IProfissionalServico profissionalServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _profissionalServico = profissionalServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ProfissionalViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid profissionalID)
        {
            Profissional? profissional = await _profissionalServico.BuscarPorID(profissionalID);
            if (profissional == null)
                return NotFound("Nenhum profissional encontrado");


            var resultado = _mapper.Map<ProfissionalViewModel>(profissional);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os profissionais pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<ProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var profissionais = await _profissionalServico.BuscarPorNome(parametro);

            if (profissionais == null || profissionais.Count == 0)
                return NotFound("Nenhum profissional encontrado");

            var resultado = _mapper.Map<List<ProfissionalViewModel>>(profissionais);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var profissionais = await _profissionalServico.BuscarTodos();

            if (profissionais == null || profissionais.Count == 0)
                return NotFound("Nenhum profissional encontrado");


            var resultado = _mapper.Map<List<ProfissionalViewModel>>(profissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um profissional
        /// </summary>
        /// <response code="202">Profissional criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Profissional atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarProfissionalInputModel body)
        {
            var (criou, profissionalId) = await _profissionalServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ProfissionalIdResponseViewModel(profissionalId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
