using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ModelosAnamneseG;
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
    public class ModeloAnamneseGController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModeloAnamneseGServico _modeloAnamneseGServico;
        private readonly IHttpContextAccessor _httpContext;

        public ModeloAnamneseGController(
            IMapper mapper,
            IModeloAnamneseGServico modeloAnamneseGServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _modeloAnamneseGServico = modeloAnamneseGServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o modelo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o modelo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ModeloAnamneseGViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int modeloAnamneseGID)
        {
            ModeloAnamneseG? modeloAnamneseG = await _modeloAnamneseGServico.BuscarPorID(modeloAnamneseGID);
            if (modeloAnamneseG == null)
                return NotFound("Nenhum modelo de anamnese encontrado");


            var resultado = _mapper.Map<ModeloAnamneseGViewModel>(modeloAnamneseG);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os modelos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseGViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var modelosAnamneseG = await _modeloAnamneseGServico.BuscarPorNome(parametro);

            if (modelosAnamneseG == null || modelosAnamneseG.Count == 0)
                return NotFound("Nenhum modelo de anamnese encontrado");

            var resultado = _mapper.Map<List<ModeloAnamneseGViewModel>>(modelosAnamneseG);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os modelos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ModeloAnamneseGViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var modelosAnamneseG = await _modeloAnamneseGServico.BuscarTodos();

            if (modelosAnamneseG == null || modelosAnamneseG.Count == 0)
                return NotFound("Nenhum modelo de anamnese encontrado");


            var resultado = _mapper.Map<List<ModeloAnamneseGViewModel>>(modelosAnamneseG);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um modelo de anamnese.
        /// </summary>         
        ///<response code="201">Modelo de anamnese criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseGViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarModeloAnamneseGInputModel modeloAnamneseG)
        {
            var retorno = await _modeloAnamneseGServico.Adicionar(_mapper.Map<ModeloAnamneseG>(modeloAnamneseG));
            return Ok(_mapper.Map<ModeloAnamneseGViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um modelo de anamnese.
        /// </summary>         
        ///<response code="200">Modelo de anamnese atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ModeloAnamneseGViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ModeloAnamneseGInputModel modeloAnamneseG)
        {
            // Busca o registro existente
            var modeloAnamneseGExistente = await _modeloAnamneseGServico.BuscarPorID(modeloAnamneseG.ModeloAnamneseGid);
            if (modeloAnamneseGExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(modeloAnamneseG, modeloAnamneseGExistente); // Faz o merge

            var retorno = await _modeloAnamneseGServico.Atualizar(_mapper.Map<ModeloAnamneseG>(modeloAnamneseGExistente));
            return Ok(_mapper.Map<ModeloAnamneseGInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um modelo de anamnese.
        /// </summary>         
        ///<response code="200">Modelo de anamnese excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int modeloAnamneseGID)
        {
            await _modeloAnamneseGServico.Deletar(modeloAnamneseGID);
            return Ok();
        }
    }
}
