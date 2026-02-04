using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseSubGrupoQuestoes;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(1.0)]
    [RequerValidacaoDeToken]
    public class AnamneseSubGrupoQuestaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseSubGrupoQuestaoServico _anamneseSubGrupoQuestaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseSubGrupoQuestaoController(
            IMapper mapper,
            IAnamneseSubGrupoQuestaoServico anamneseSubGrupoQuestaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseSubGrupoQuestaoServico = anamneseSubGrupoQuestaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a questão do subgrupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a questão do subgrupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AnamneseSubGrupoQuestaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseSubGrupoQuestaoID)
        {
            AnamneseSubGrupoQuestao? anamneseSubGrupoQuestao = await _anamneseSubGrupoQuestaoServico.BuscarPorID(anamneseSubGrupoQuestaoID);
            if (anamneseSubGrupoQuestao == null)
                return NotFound("Nenhuma questão do subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<AnamneseSubGrupoQuestaoViewModel>(anamneseSubGrupoQuestao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as questões dos subgrupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoQuestaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseSubGrupoQuestao = await _anamneseSubGrupoQuestaoServico.BuscarPorNome(parametro);

            if (anamneseSubGrupoQuestao == null || anamneseSubGrupoQuestao.Count == 0)
                return NotFound("Nenhuma questão do subgrupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseSubGrupoQuestaoViewModel>>(anamneseSubGrupoQuestao);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as questões dos subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoQuestaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseSubGrupoQuestao = await _anamneseSubGrupoQuestaoServico.BuscarTodos();

            if (anamneseSubGrupoQuestao == null || anamneseSubGrupoQuestao.Count == 0)
                return NotFound("Nenhuma questão do subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseSubGrupoQuestaoViewModel>>(anamneseSubGrupoQuestao);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma questão do subgrupo.
        /// </summary>         
        ///<response code="201">Questão do subgrupo criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoQuestaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAnamneseSubGrupoQuestaoInputModel anamneseSubGrupoQuestao)
        {
            var retorno = await _anamneseSubGrupoQuestaoServico.Adicionar(_mapper.Map<AnamneseSubGrupoQuestao>(anamneseSubGrupoQuestao));
            return Ok(_mapper.Map<AnamneseSubGrupoQuestaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma questão do subgrupo.
        /// </summary>         
        ///<response code="200">Questão do subgrupo atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoQuestaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AnamneseSubGrupoQuestaoInputModel anamneseSubGrupoQuestao)
        {
            // Busca o registro existente
            var anamneseSubGrupoQuestaoExistente = await _anamneseSubGrupoQuestaoServico.BuscarPorID(anamneseSubGrupoQuestao.AnamneseSubGrupoQuestaoId);
            if (anamneseSubGrupoQuestaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(anamneseSubGrupoQuestao, anamneseSubGrupoQuestaoExistente); // Faz o merge

            var retorno = await _anamneseSubGrupoQuestaoServico.Atualizar(_mapper.Map<AnamneseSubGrupoQuestao>(anamneseSubGrupoQuestaoExistente));
            return Ok(_mapper.Map<AnamneseSubGrupoQuestaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma questão do subgrupo.
        /// </summary>         
        ///<response code="200">Questão do subgrupo excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int anamneseSubGrupoQuestaoID)
        {
            await _anamneseSubGrupoQuestaoServico.Deletar(anamneseSubGrupoQuestaoID);
            return Ok();
        }
    }
}
