using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseGrupos;
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
    public class AnamneseGrupoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseGrupoServico _anamneseGrupoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseGrupoController(
            IMapper mapper,
            IAnamneseGrupoServico anamneseGrupoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseGrupoServico = anamneseGrupoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o grupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o grupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AnamneseGrupoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseGrupoID)
        {
            AnamneseGrupo? anamneseGrupo = await _anamneseGrupoServico.BuscarPorID(anamneseGrupoID);
            if (anamneseGrupo == null)
                return NotFound("Nenhum grupo de anamnese encontrado");


            var resultado = _mapper.Map<AnamneseGrupoViewModel>(anamneseGrupo);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os grupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseGrupo = await _anamneseGrupoServico.BuscarPorNome(parametro);

            if (anamneseGrupo == null || anamneseGrupo.Count == 0)
                return NotFound("Nenhum grupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseGrupoViewModel>>(anamneseGrupo);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os grupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AnamneseGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseGrupo = await _anamneseGrupoServico.BuscarTodos();

            if (anamneseGrupo == null || anamneseGrupo.Count == 0)
                return NotFound("Nenhum grupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseGrupoViewModel>>(anamneseGrupo);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um grupo de anamnese
        /// </summary>
        /// <response code="202">Grupo de anamnese criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Grupo de anamnese atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseGrupoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAnamneseGrupoInputModel body)
        {
            var (criou, anamneseGrupoId) = await _anamneseGrupoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AnamneseGrupoIdResponseViewModel(anamneseGrupoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
