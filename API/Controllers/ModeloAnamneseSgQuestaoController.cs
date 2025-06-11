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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorNome")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria uma questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="201">Questão do subgrupo do grupo do modelo de anamnese criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgQuestaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarModeloAnamneseSgQuestaoInputModel modeloAnamneseSgQuestao)
        {
            var retorno = await _modeloAnamneseSgQuestaoServico.Adicionar(_mapper.Map<ModeloAnamneseSgQuestao>(modeloAnamneseSgQuestao));
            return Ok(_mapper.Map<ModeloAnamneseSgQuestaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Questão do subgrupo do grupo do modelo de anamnese atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgQuestaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ModeloAnamneseSgQuestaoInputModel modeloAnamneseSgQuestao)
        {
            // Busca o registro existente
            var modeloAnamneseSgQuestaoExistente = await _modeloAnamneseSgQuestaoServico.BuscarPorID(modeloAnamneseSgQuestao.ModeloAnamneseSgQuestaoId);
            if (modeloAnamneseSgQuestaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(modeloAnamneseSgQuestao, modeloAnamneseSgQuestaoExistente); // Faz o merge

            var retorno = await _modeloAnamneseSgQuestaoServico.Atualizar(_mapper.Map<ModeloAnamneseSgQuestao>(modeloAnamneseSgQuestaoExistente));
            return Ok(_mapper.Map<ModeloAnamneseSgQuestaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Questão do subgrupo do grupo do modelo de anamnese excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _modeloAnamneseSgQuestaoServico.Deletar(id);
            return Ok();
        }
    }
}
