using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ProfissionaisAcessos;
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
    public class ProfissionalAcessoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfissionalAcessoServico _profissionalAcessoServico;
        private readonly IHttpContextAccessor _httpContext;

        public ProfissionalAcessoController(
            IMapper mapper,
            IProfissionalAcessoServico profissionalAcessoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _profissionalAcessoServico = profissionalAcessoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o acesso profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o acesso do profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ProfissionalAcessoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int profissionalAcessoID)
        {
            ProfissionalAcesso? profissionalAcesso = await _profissionalAcessoServico.BuscarPorID(profissionalAcessoID);
            if (profissionalAcesso == null)
                return NotFound("Nenhum acesso profissional encontrado");


            var resultado = _mapper.Map<ProfissionalAcessoViewModel>(profissionalAcesso);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca o acesso profissional a partir do identificador do profissional informado
        /// </summary>
        /// <response code="200">Retorna o acesso do profissional pelo ID do profissional informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorIDProfissional")]
        [ProducesResponseType(
            typeof(ProfissionalAcessoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorIDProfissional(Guid profissionalID)
        {
            var acessosprofissionais = await _profissionalAcessoServico.BuscarPorIDProfissional(profissionalID);

            if (acessosprofissionais == null || acessosprofissionais.Count == 0)
                return NotFound("Nenhum acesso para o profissional encontrado");

            var resultado = _mapper.Map<List<ProfissionalAcessoViewModel>>(acessosprofissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acessos profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ProfissionalAcessoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var acessosprofissionais = await _profissionalAcessoServico.BuscarTodos();

            if (acessosprofissionais == null || acessosprofissionais.Count == 0)
                return NotFound("Nenhum acesso para o profissional encontrado");


            var resultado = _mapper.Map<List<ProfissionalAcessoViewModel>>(acessosprofissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um acesso do profissional
        /// </summary>
        /// <response code="202">Acesso do profissional criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Acesso do profissional atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalAcessoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarProfissionalAcessoInputModel body)
        {
            var (criou, profissionalAcessoId) = await _profissionalAcessoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ProfissionalAcessoIdResponseViewModel(profissionalAcessoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
