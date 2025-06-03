using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ModelosAnamneseSG;
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
    public class ModeloAnamneseSgController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModeloAnamneseSgServico _modeloAnamneseSgServico;
        private readonly IHttpContextAccessor _httpContext;

        public ModeloAnamneseSgController(
            IMapper mapper,
            IModeloAnamneseSgServico modeloAnamneseSgServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _modeloAnamneseSgServico = modeloAnamneseSgServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o grupo do modelo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o grupo do modelo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(ModeloAnamneseSgViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int modeloAnamneseSgID)
        {
            ModeloAnamneseSg? modeloAnamneseSg = await _modeloAnamneseSgServico.BuscarPorID(modeloAnamneseSgID);
            if (modeloAnamneseSg == null)
                return NotFound("Nenhum grupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<ModeloAnamneseSgViewModel>(modeloAnamneseSg);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os grupos dos modelos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorNome")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var modelosAnamneseSg = await _modeloAnamneseSgServico.BuscarPorNome(parametro);

            if (modelosAnamneseSg == null || modelosAnamneseSg.Count == 0)
                return NotFound("Nenhum grupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgViewModel>>(modelosAnamneseSg);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var modelosAnamneseSg = await _modeloAnamneseSgServico.BuscarTodos();

            if (modelosAnamneseSg == null || modelosAnamneseSg.Count == 0)
                return NotFound("Nenhum grupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<List<ModeloAnamneseSgViewModel>>(modelosAnamneseSg);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um grupo do modelo de anamnese
        /// </summary>
        /// <response code="202">Grupo do modelo de anamnese criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Grupo do modelo de anamnese atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarModeloAnamneseSgInputModel body)
        {
            var (criou, modeloAnamneseSgId) = await _modeloAnamneseSgServico.CriarOuAtualizar(body, true);

            if (criou)            
                return Accepted(new ModeloAnamneseSgIdResponseViewModel(modeloAnamneseSgId));
            
            return NoContent(); // Atualizado com sucesso, sem corpo

        }
    }
}
