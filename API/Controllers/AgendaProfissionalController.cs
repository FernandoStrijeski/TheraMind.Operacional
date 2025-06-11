using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AgendasProfissionais;
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
    public class AgendaProfissionalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaProfissionalServico _agendaProfissionalServico;
        private readonly IHttpContextAccessor _httpContext;

        public AgendaProfissionalController(
            IMapper mapper,
            IAgendaProfissionalServico agendaProfissionalServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _agendaProfissionalServico = agendaProfissionalServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a agenda do profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a agenda do profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AgendaProfissionalViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int agendaProfissionalID)
        {
            AgendaProfissional? agendaProfissional = await _agendaProfissionalServico.BuscarPorID(agendaProfissionalID);
            if (agendaProfissional == null)
                return NotFound("Nenhuma agenda do profissional encontrada");


            var resultado = _mapper.Map<AgendaProfissionalViewModel>(agendaProfissional);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as agendas dos profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AgendaProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var agendasProfissionais = await _agendaProfissionalServico.BuscarTodos();

            if (agendasProfissionais == null || agendasProfissionais.Count == 0)
                return NotFound("Nenhuma agenda do profissional encontrada");


            var resultado = _mapper.Map<List<AgendaProfissionalViewModel>>(agendasProfissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria uma agenda profissional.
        /// </summary>         
        ///<response code="201">Agenda profissional criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaProfissionalViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAgendaProfissionalInputModel agendaProfissional)
        {
            var retorno = await _agendaProfissionalServico.Adicionar(_mapper.Map<AgendaProfissional>(agendaProfissional));
            return Ok(_mapper.Map<AgendaProfissionalViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma agenda profissional.
        /// </summary>         
        ///<response code="200">Agenda profissional atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AgendaProfissionalViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AgendaProfissionalInputModel agendaProfissional)
        {
            // Busca o registro existente
            var agendaProfissionalExistente = await _agendaProfissionalServico.BuscarPorID(agendaProfissional.AgendaProfissionalId);
            if (agendaProfissionalExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(agendaProfissional, agendaProfissionalExistente); // Faz o merge

            var retorno = await _agendaProfissionalServico.Atualizar(_mapper.Map<AgendaProfissional>(agendaProfissionalExistente));
            return Ok(_mapper.Map<AgendaProfissionalInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma agenda profissional.
        /// </summary>         
        ///<response code="200">Agenda profissional excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _agendaProfissionalServico.Deletar(id);
            return Ok();
        }
    }
}
