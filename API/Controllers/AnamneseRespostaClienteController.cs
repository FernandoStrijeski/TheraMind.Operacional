using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseRespostaClientes;
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
    public class AnamneseRespostaClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseRespostaClienteServico _anamneseRespostaClienteServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseRespostaClienteController(
            IMapper mapper,
            IAnamneseRespostaClienteServico anamneseRespostaClienteServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseRespostaClienteServico = anamneseRespostaClienteServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a resposta da questão do subgrupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a resposta da questão do subgrupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AnamneseRespostaClienteViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseSubGrupoQuestaoID)
        {
            AnamneseRespostaCliente? anamneseRespostaCliente = await _anamneseRespostaClienteServico.BuscarPorID(anamneseSubGrupoQuestaoID);
            if (anamneseRespostaCliente == null)
                return NotFound("Nenhuma resposta da questão do subgrupo de anamnese encontrada");


            var resultado = _mapper.Map<AnamneseRespostaClienteViewModel>(anamneseRespostaCliente);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as respostas das questões dos subgrupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseRespostaClienteViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseRespostaCliente = await _anamneseRespostaClienteServico.BuscarPorNome(parametro);

            if (anamneseRespostaCliente == null || anamneseRespostaCliente.Count == 0)
                return NotFound("Nenhuma resposta da questão do subgrupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseRespostaClienteViewModel>>(anamneseRespostaCliente);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as respostas das questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AnamneseRespostaClienteViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseRespostaCliente = await _anamneseRespostaClienteServico.BuscarTodos();

            if (anamneseRespostaCliente == null || anamneseRespostaCliente.Count == 0)
                return NotFound("Nenhuma resposta da questão do subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseRespostaClienteViewModel>>(anamneseRespostaCliente);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma resposta de questão do subgrupo de anamnese
        /// </summary>
        /// <response code="202">Resposa da questão do subgrupo de anamnese criada com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Resposta da questão do subgrupo de anamnese atualizada com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseRespostaClienteIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarAnamneseRespostaClienteInputModel body)
        {
            var (criou, anamneseSubGrupoQuestaoId) = await _anamneseRespostaClienteServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new AnamneseRespostaClienteIdResponseViewModel(anamneseSubGrupoQuestaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
