using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ModelosAnamneseSGQuestaoOpcoes;
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
    public class ModeloAnamneseSgQuestaoOpcaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModeloAnamneseSgQuestaoOServico _modeloAnamneseSgQuestaoOServico;
        private readonly IHttpContextAccessor _httpContext;

        public ModeloAnamneseSgQuestaoOpcaoController(
            IMapper mapper,
            IModeloAnamneseSgQuestaoOServico modeloAnamneseSgQuestaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _modeloAnamneseSgQuestaoOServico = modeloAnamneseSgQuestaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a opção da questão do subgrupo do grupo do modelo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a opção da questão do subgrupo do grupo do modelo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ModeloAnamneseSgQuestaoOViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int modeloAnamneseSgQuestaoOID)
        {
            ModeloAnamneseSgQuestaoO? modeloAnamneseSgQuestaoO = await _modeloAnamneseSgQuestaoOServico.BuscarPorID(modeloAnamneseSgQuestaoOID);
            if (modeloAnamneseSgQuestaoO == null)
                return NotFound("Nenhuma opção da questão do subgrupo do grupo do modelo de anamnese encontrado");


            var resultado = _mapper.Map<ModeloAnamneseSgQuestaoOViewModel>(modeloAnamneseSgQuestaoO);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as opções das questões dos subgrupos dos grupos dos modelos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgQuestaoOViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var modelosAnamneseSgQuestaoO = await _modeloAnamneseSgQuestaoOServico.BuscarPorNome(parametro);

            if (modelosAnamneseSgQuestaoO == null || modelosAnamneseSgQuestaoO.Count == 0)
                return NotFound("Nenhuma opção da questão do subgrupo do grupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgQuestaoOViewModel>>(modelosAnamneseSgQuestaoO);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as opções das questões dos subgrupos dos grupos dos modelos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseSgQuestaoOViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var modelosAnamneseSgQuestaoO = await _modeloAnamneseSgQuestaoOServico.BuscarTodos();

            if (modelosAnamneseSgQuestaoO == null || modelosAnamneseSgQuestaoO.Count == 0)
                return NotFound("Nenhuma opção da questão do subgrupo do grupo do modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseSgQuestaoOViewModel>>(modelosAnamneseSgQuestaoO);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma opção da questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="201">Opção da questão do subgrupo do grupo do modelo de anamnese criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgQuestaoOViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarModeloAnamneseSgQuestaoOInputModel modeloAnamneseSgQuestaoO)
        {
            var retorno = await _modeloAnamneseSgQuestaoOServico.Adicionar(_mapper.Map<ModeloAnamneseSgQuestaoO>(modeloAnamneseSgQuestaoO));
            return Ok(_mapper.Map<ModeloAnamneseSgQuestaoOViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma opção da questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Opção da questão do subgrupo do grupo do modelo de anamnese atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseSgQuestaoOViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ModeloAnamneseSgQuestaoOInputModel modeloAnamneseSgQuestaoO)
        {
            // Busca o registro existente
            var modeloAnamneseSgQuestaoOExistente = await _modeloAnamneseSgQuestaoOServico.BuscarPorID(modeloAnamneseSgQuestaoO.ModeloAnamneseSgQuestaoOid);
            if (modeloAnamneseSgQuestaoOExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(modeloAnamneseSgQuestaoO, modeloAnamneseSgQuestaoOExistente); // Faz o merge

            var retorno = await _modeloAnamneseSgQuestaoOServico.Atualizar(_mapper.Map<ModeloAnamneseSgQuestaoO>(modeloAnamneseSgQuestaoOExistente));
            return Ok(_mapper.Map<ModeloAnamneseSgQuestaoOInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma opção da questão do subgrupo do grupo do modelo de anamnese.
        /// </summary>         
        ///<response code="200">Opção da questão do subgrupo do grupo do modelo de anamnese excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _modeloAnamneseSgQuestaoOServico.Deletar(id);
            return Ok();
        }
    }
}
