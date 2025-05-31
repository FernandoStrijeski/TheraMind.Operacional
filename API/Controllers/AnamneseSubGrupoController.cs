using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseSubGrupos;
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
    public class AnamneseSubGrupoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseSubGrupoServico _anamneseSubGrupoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseSubGrupoController(
            IMapper mapper,
            IAnamneseSubGrupoServico anamneseSubGrupoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseSubGrupoServico = anamneseSubGrupoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o subgrupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o subgrupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(AnamneseSubGrupoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseSubGrupoID)
        {
            AnamneseSubGrupo? anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarPorID(anamneseSubGrupoID);
            if (anamneseSubGrupo == null)
                return NotFound("Nenhum subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<AnamneseSubGrupoViewModel>(anamneseSubGrupo);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os subgrupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarPorNome(parametro);

            if (anamneseSubGrupo == null || anamneseSubGrupo.Count == 0)
                return NotFound("Nenhum subgrupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseSubGrupoViewModel>>(anamneseSubGrupo);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarTodos();

            if (anamneseSubGrupo == null || anamneseSubGrupo.Count == 0)
                return NotFound("Nenhum subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseSubGrupoViewModel>>(anamneseSubGrupo);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um subgrupo de anamnese
        /// </summary>
        /// <response code="202">Subgrupo de anamnese criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Subgrupo de anamnese atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAnamneseSubGrupoInputModel body)
        {
            var (criou, anamneseSubGrupoId) = await _anamneseSubGrupoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AnamneseSubGrupoIdResponseViewModel(anamneseSubGrupoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
