using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ModelosAnamneseSGQuestoes;
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
    public class ModeloAnamneseSgQuestaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModeloAnamneseSgQuestaoServico _modeloAnamneseSgQuestaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public ModeloAnamneseSgQuestaoController(
            IMapper mapper,
            IModeloAnamneseSgQuestaoServico modeloAnamneseSgQuestaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _modeloAnamneseSgQuestaoServico = modeloAnamneseSgQuestaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a questão do subgrupo do grupo do modelo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a questão do subgrupo do grupo do modelo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ModeloAnamneseSgQuestaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int modeloAnamneseSgQuestaoID)
        {
            ModeloAnamneseSgQuestao? modeloAnamneseSgQuestao = await _modeloAnamneseSgQuestaoServico.BuscarPorID(modeloAnamneseSgQuestaoID);
            if (modeloAnamneseSgQuestao == null)
                return NotFound("Nenhuma questão do subgrupo do grupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<ModeloAnamneseSgQuestaoViewModel>(modeloAnamneseSgQuestao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as questões dos subgrupos dos grupos dos modelos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgQuestaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var modelosAnamneseSgQuestao = await _modeloAnamneseSgQuestaoServico.BuscarPorNome(parametro);

            if (modelosAnamneseSgQuestao == null || modelosAnamneseSgQuestao.Count == 0)
                return NotFound("Nenhuma questão do subgrupo do grupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgQuestaoViewModel>>(modelosAnamneseSgQuestao);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as questões dos subgrupos dos grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgQuestaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var modelosAnamneseSgQuestao = await _modeloAnamneseSgQuestaoServico.BuscarTodos();

            if (modelosAnamneseSgQuestao == null || modelosAnamneseSgQuestao.Count == 0)
                return NotFound("Nenhuma questão do subgrupo do grupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgQuestaoViewModel>>(modelosAnamneseSgQuestao);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza uma questão do subgrupo do grupo do modelo de anamnese
        /// </summary>
        /// <response code="202">Questão do subgrupo do grupo do modelo de anamnese criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Questão do subgrupo do grupo do modelo de anamnese atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgQuestaoIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarModeloAnamneseSgQuestaoInputModel body)
        {
            var (criou, modeloAnamneseSgQuestaoId) = await _modeloAnamneseSgQuestaoServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ModeloAnamneseSgQuestaoIdResponseViewModel(modeloAnamneseSgQuestaoId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
