using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasSessoes;
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
    public class AgendaSessaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaSessaoServico _agendaSessaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaSessaoController(
            IMapper mapper,
            IAgendaSessaoServico agendaSessaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaSessaoServico = agendaSessaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a sessão da agenda a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a sessão da agenda pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AgendaSessaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid agendaSessaoID)
        {
            AgendaSessao? agendaSessao = await _agendaSessaoServico.BuscarPorID(agendaSessaoID);
            if (agendaSessao == null)
                return NotFound("Nenhuma sessão da agenda encontrada");


            var resultado = _mapper.Map<AgendaSessaoViewModel>(agendaSessao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as sessões da agenda
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var agendaSessoes = await _agendaSessaoServico.BuscarPorNome(parametro);

            if (agendaSessoes == null || agendaSessoes.Count == 0)
                return NotFound("Nenhuma sessão da agenda encontrada");

            var resultado = _mapper.Map<List<AgendaSessaoViewModel>>(agendaSessoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as sessões da agenda
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendaSessoes = await _agendaSessaoServico.BuscarTodos();

            if (agendaSessoes == null || agendaSessoes.Count == 0)
                return NotFound("Nenhuma sessão da agenda encontrada");


            var resultado = _mapper.Map<List<AgendaSessaoViewModel>>(agendaSessoes);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma sessão da agenda.
        /// </summary>         
        ///<response code="201">Sessão da agenda criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAgendaSessaoInputModel agendaSessao)
        {
            var retorno = await _agendaSessaoServico.Adicionar(_mapper.Map<AgendaSessao>(agendaSessao));
            return Ok(_mapper.Map<AgendaSessaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma sessão da agenda.
        /// </summary>         
        ///<response code="200">Sessão da agenda atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AgendaSessaoInputModel agendaSessao)
        {
            // Busca o registro existente
            var agendaSessaoExistente = await _agendaSessaoServico.BuscarPorID(agendaSessao.AgendaSessaoId);
            if (agendaSessaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(agendaSessao, agendaSessaoExistente); // Faz o merge

            var retorno = await _agendaSessaoServico.Atualizar(_mapper.Map<AgendaSessao>(agendaSessaoExistente));
            return Ok(_mapper.Map<AgendaSessaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma sessão da agenda.
        /// </summary>         
        ///<response code="200">Sessão da agenda excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            await _agendaSessaoServico.Deletar(id);
            return Ok();
        }
    }
}
