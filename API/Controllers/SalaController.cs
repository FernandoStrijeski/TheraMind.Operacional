using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Salas;
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
    public class SalaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISalaServico _salaServico;
        private readonly IHttpContextAccessor _httpContext;

        public SalaController(
            IMapper mapper,
            ISalaServico salaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _salaServico = salaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a sala a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a sala pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(SalaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(string salaID)
        {
            Sala? sala = await _salaServico.BuscarPorID(salaID);
            if (sala == null)
                return NotFound("Nenhuma sala encontrada");


            var resultado = _mapper.Map<SalaViewModel>(sala);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as salas pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<SalaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var salas = await _salaServico.BuscarPorNome(parametro);

            if (salas == null || salas.Count == 0)
                return NotFound("Nenhuma sala encontrada");

            var resultado = _mapper.Map<List<SalaViewModel>>(salas);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as salas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<SalaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var salas = await _salaServico.BuscarTodos();

            if (salas == null || salas.Count == 0)
                return NotFound("Nenhuma sala encontrada");


            var resultado = _mapper.Map<List<SalaViewModel>>(salas);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma sala.
        /// </summary>         
        ///<response code="201">Sala criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SalaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarSalaInputModel sala)
        {
            var retorno = await _salaServico.Adicionar(_mapper.Map<Sala>(sala));
            return Ok(_mapper.Map<SalaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma sala.
        /// </summary>         
        ///<response code="200">Sala atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(SalaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] SalaInputModel sala)
        {
            // Busca o registro existente
            var salaExistente = await _salaServico.BuscarPorID(sala.SalaId);
            if (salaExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(sala, salaExistente); // Faz o merge

            var retorno = await _salaServico.Atualizar(_mapper.Map<Sala>(salaExistente));
            return Ok(_mapper.Map<SalaViewModel>(retorno));
        }

        /// <summary>
        /// Exclui uma sala.
        /// </summary>         
        ///<response code="200">Sala excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] string salaID)
        {
            await _salaServico.Deletar(salaID);
            return Ok();
        }
    }    
}
