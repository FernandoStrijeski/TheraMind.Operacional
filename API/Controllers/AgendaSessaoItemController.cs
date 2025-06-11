using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasSessaoItens;
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
    public class AgendaSessaoItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaSessaoItemServico _agendaSessaoItemServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaSessaoItemController(
            IMapper mapper,
            IAgendaSessaoItemServico agendaSessaoItemServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaSessaoItemServico = agendaSessaoItemServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o item da sessão da agenda a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o item da sessão da agenda pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AgendaSessaoItemViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int agendaSessaoItemID)
        {
            AgendaSessaoItem? agendaSessaoItem = await _agendaSessaoItemServico.BuscarPorID(agendaSessaoItemID);
            if (agendaSessaoItem == null)
                return NotFound("Nenhum item da sessão da agenda encontrada");


            var resultado = _mapper.Map<AgendaSessaoItemViewModel>(agendaSessaoItem);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os itens das sessões da agenda
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoItemViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var agendaSessaoItens = await _agendaSessaoItemServico.BuscarPorNome(parametro);

            if (agendaSessaoItens == null || agendaSessaoItens.Count == 0)
                return NotFound("Nenhum item da sessão da agenda encontrada");

            var resultado = _mapper.Map<List<AgendaSessaoItemViewModel>>(agendaSessaoItens);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os itens das sessões da agenda
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AgendaSessaoItemViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendaSessoaoItens = await _agendaSessaoItemServico.BuscarTodos();

            if (agendaSessoaoItens == null || agendaSessoaoItens.Count == 0)
                return NotFound("Nenhum item da sessão da agenda encontrada");


            var resultado = _mapper.Map<List<AgendaSessaoItemViewModel>>(agendaSessoaoItens);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um item da sessão da agenda.
        /// </summary>         
        ///<response code="201">Item da sessão da agenda criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoItemViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAcompanhamentoClinicoInputModel agendaSessaoItem)
        {
            var retorno = await _agendaSessaoItemServico.Adicionar(_mapper.Map<AgendaSessaoItem>(agendaSessaoItem));
            return Ok(_mapper.Map<AgendaSessaoItemViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um item da sessão da agenda.
        /// </summary>         
        ///<response code="200">item da sessão da agenda atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaSessaoItemViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AgendaSessaoItemInputModel agendaSessaoItem)
        {
            // Busca o registro existente
            var agendaSessaoItemExistente = await _agendaSessaoItemServico.BuscarPorID(agendaSessaoItem.AgendaSessaoItemId);
            if (agendaSessaoItemExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(agendaSessaoItem, agendaSessaoItemExistente); // Faz o merge

            var retorno = await _agendaSessaoItemServico.Atualizar(_mapper.Map<AgendaSessaoItem>(agendaSessaoItemExistente));
            return Ok(_mapper.Map<AgendaSessaoItemInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um item da sessão da agenda.
        /// </summary>         
        ///<response code="200">Item da sessão da agenda excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _agendaSessaoItemServico.Deletar(id);
            return Ok();
        }
    }
}
