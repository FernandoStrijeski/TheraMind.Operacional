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
        /// Cria uma resposta da questão.
        /// </summary>         
        ///<response code="201">Resposta da questão criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseRespostaClienteViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAnamneseRespostaClienteInputModel anamneseRespostaCliente)
        {
            var retorno = await _anamneseRespostaClienteServico.Adicionar(_mapper.Map<AnamneseRespostaCliente>(anamneseRespostaCliente));
            return Ok(_mapper.Map<AnamneseRespostaClienteViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma resposta da questão.
        /// </summary>         
        ///<response code="200">Resposta da questão atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseRespostaClienteViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AnamneseRespostaClienteInputModel anamneseRespostaCliente)
        {
            // Busca o registro existente
            var anamneseRespostaClienteExistente = await _anamneseRespostaClienteServico.BuscarPorID(anamneseRespostaCliente.AnamneseSubGrupoQuestaoId);
            if (anamneseRespostaClienteExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(anamneseRespostaCliente, anamneseRespostaClienteExistente); // Faz o merge

            var retorno = await _anamneseRespostaClienteServico.Atualizar(_mapper.Map<AnamneseRespostaCliente>(anamneseRespostaClienteExistente));
            return Ok(_mapper.Map<AnamneseRespostaClienteInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma resposta da questão.
        /// </summary>         
        ///<response code="200">Resposta da questão excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _anamneseRespostaClienteServico.Deletar(id);
            return Ok();
        }
    }
}
