using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseSubGrupoQuestaoOpcoes;
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
    public class AnamneseSubGrupoQuestaoOpcaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseSubGrupoQuestaoOpcaoServico _anamneseSubGrupoQuestaoOpcaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseSubGrupoQuestaoOpcaoController(
            IMapper mapper,
            IAnamneseSubGrupoQuestaoOpcaoServico anamneseSubGrupoQuestaoOpcaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseSubGrupoQuestaoOpcaoServico = anamneseSubGrupoQuestaoOpcaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a opção da questão do subgrupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a opção da questão do subgrupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(AnamneseSubGrupoQuestaoOpcaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseSubGrupoQuestaoOpcaoID)
        {
            AnamneseSubGrupoQuestaoOpcao? anamneseSubGrupoQuestaoOpcao = await _anamneseSubGrupoQuestaoOpcaoServico.BuscarPorID(anamneseSubGrupoQuestaoOpcaoID);
            if (anamneseSubGrupoQuestaoOpcao == null)
                return NotFound("Nenhuma opção da questão do subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<AnamneseSubGrupoQuestaoOpcaoViewModel>(anamneseSubGrupoQuestaoOpcao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as opções das questões dos subgrupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoQuestaoOpcaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseSubGrupoQuestaoOpcao = await _anamneseSubGrupoQuestaoOpcaoServico.BuscarPorNome(parametro);

            if (anamneseSubGrupoQuestaoOpcao == null || anamneseSubGrupoQuestaoOpcao.Count == 0)
                return NotFound("Nenhuma opção da questão do subgrupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseSubGrupoQuestaoOpcaoViewModel>>(anamneseSubGrupoQuestaoOpcao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as opções das questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoQuestaoOpcaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseSubGrupoQuestaoOpcao = await _anamneseSubGrupoQuestaoOpcaoServico.BuscarTodos();

            if (anamneseSubGrupoQuestaoOpcao == null || anamneseSubGrupoQuestaoOpcao.Count == 0)
                return NotFound("Nenhuma opção da questão do subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseSubGrupoQuestaoOpcaoViewModel>>(anamneseSubGrupoQuestaoOpcao);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma opção da questão do subgrupo de anamnese
        /// </summary>
        /// <response code="202">Opção da questão do subgrupo de anamnese criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Opção da questão do subgrupo de anamnese atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoQuestaoOpcaoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAnamneseSubGrupoQuestaoOpcaoInputModel body)
        {
            var (criou, anamneseSubGrupoQuestaoOpcaoId) = await _anamneseSubGrupoQuestaoOpcaoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AnamneseSubGrupoQuestaoOpcaoIdResponseViewModel(anamneseSubGrupoQuestaoOpcaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
