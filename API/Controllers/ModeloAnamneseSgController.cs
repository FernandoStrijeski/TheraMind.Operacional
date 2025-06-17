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
        /// Busca o subgrupo do modelo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o subgrupo do modelo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ModeloAnamneseSgViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int modeloAnamneseSgID)
        {
            ModeloAnamneseSg? modeloAnamneseSg = await _modeloAnamneseSgServico.BuscarPorID(modeloAnamneseSgID);
            if (modeloAnamneseSg == null)
                return NotFound("Nenhum subgrupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<ModeloAnamneseSgViewModel>(modeloAnamneseSg);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os subgrupos dos modelos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var modelosAnamneseSg = await _modeloAnamneseSgServico.BuscarPorNome(parametro);

            if (modelosAnamneseSg == null || modelosAnamneseSg.Count == 0)
                return NotFound("Nenhum subgrupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgViewModel>>(modelosAnamneseSg);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os subgrupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var modelosAnamneseSg = await _modeloAnamneseSgServico.BuscarTodos();

            if (modelosAnamneseSg == null || modelosAnamneseSg.Count == 0)
                return NotFound("Nenhum subgrupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<List<ModeloAnamneseSgViewModel>>(modelosAnamneseSg);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um subgrupo do modelo de anamnese.
        /// </summary>         
        ///<response code="201">Subgrupo do modelo de anamnese criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarModeloAnamneseSgInputModel modeloAnamneseSg)
        {
            var retorno = await _modeloAnamneseSgServico.Adicionar(_mapper.Map<ModeloAnamneseSg>(modeloAnamneseSg));
            return Ok(_mapper.Map<ModeloAnamneseSgViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um subgrupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Subgrupo do modelo de anamnese atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ModeloAnamneseSgInputModel modeloAnamneseSg)
        {
            // Busca o registro existente
            var modeloAnamneseSgExistente = await _modeloAnamneseSgServico.BuscarPorID(modeloAnamneseSg.ModeloAnamneseSgId);
            if (modeloAnamneseSgExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(modeloAnamneseSg, modeloAnamneseSgExistente); // Faz o merge

            var retorno = await _modeloAnamneseSgServico.Atualizar(_mapper.Map<ModeloAnamneseSg>(modeloAnamneseSgExistente));
            return Ok(_mapper.Map<ModeloAnamneseSgInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um subgrupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Subgrupo do modelo de anamnese excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int modeloAnamneseSgID)
        {
            await _modeloAnamneseSgServico.Deletar(modeloAnamneseSgID);
            return Ok();
        }
    }
}
