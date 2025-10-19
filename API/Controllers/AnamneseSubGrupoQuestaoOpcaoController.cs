using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseSubGrupoQuestaoOpcoes;
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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorNome")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria uma opção da questão do subgrupo de anamnese.
        /// </summary>         
        ///<response code="201">Opção da questão do subgrupo de anamnese criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoQuestaoOpcaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAnamneseSubGrupoQuestaoOpcaoInputModel anamneseSubGrupoQuestaoOpcao)
        {
            var retorno = await _anamneseSubGrupoQuestaoOpcaoServico.Adicionar(_mapper.Map<AnamneseSubGrupoQuestaoOpcao>(anamneseSubGrupoQuestaoOpcao));
            return Ok(_mapper.Map<AnamneseSubGrupoQuestaoOpcaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma opção da questão do subgrupo de anamnese.
        /// </summary>         
        ///<response code="200">Opção da questão do subgrupo de anamnese atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoQuestaoOpcaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AnamneseSubGrupoQuestaoOpcaoInputModel anamneseSubGrupoQuestaoOpcao)
        {
            // Busca o registro existente
            var anamneseSubGrupoQuestaoOpcaoExistente = await _anamneseSubGrupoQuestaoOpcaoServico.BuscarPorID(anamneseSubGrupoQuestaoOpcao.AnamneseSubGrupoQuestaoId);
            if (anamneseSubGrupoQuestaoOpcaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(anamneseSubGrupoQuestaoOpcao, anamneseSubGrupoQuestaoOpcaoExistente); // Faz o merge

            var retorno = await _anamneseSubGrupoQuestaoOpcaoServico.Atualizar(_mapper.Map<AnamneseSubGrupoQuestaoOpcao>(anamneseSubGrupoQuestaoOpcaoExistente));
            return Ok(_mapper.Map<AnamneseSubGrupoQuestaoOpcaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma opção da questão do subgrupo de anamnese.
        /// </summary>         
        ///<response code="200">Opção da questão do subgrupo de anamnese excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int anamneseSubGrupoQuestaoOpcaoID)
        {
            await _anamneseSubGrupoQuestaoOpcaoServico.Deletar(anamneseSubGrupoQuestaoOpcaoID);
            return Ok();
        }
    }
}
