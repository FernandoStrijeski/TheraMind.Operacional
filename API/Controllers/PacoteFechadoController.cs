using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.PacotesFechados;
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
    public class PacoteFechadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPacoteFechadoServico _pacoteFechadoServico;
        private readonly IHttpContextAccessor _httpContext;

        public PacoteFechadoController(
            IMapper mapper,
            IPacoteFechadoServico pacoteFechadoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _pacoteFechadoServico = pacoteFechadoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o pacote fechado a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o pacote fechado pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(PacoteFechadoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int pacoteFechadoID)
        {
            PacoteFechado? pacoteFechado = await _pacoteFechadoServico.BuscarPorID(pacoteFechadoID);
            if (pacoteFechado == null)
                return NotFound("Nenhum pacote fechado encontrado");


            var resultado = _mapper.Map<PacoteFechadoViewModel>(pacoteFechado);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os pacotes fechados
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<PacoteFechadoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var pacotesFechados = await _pacoteFechadoServico.BuscarTodos();

            if (pacotesFechados == null || pacotesFechados.Count == 0)
                return NotFound("Nenhum pacote fechado encontrado");


            var resultado = _mapper.Map<List<PacoteFechadoViewModel>>(pacotesFechados);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um pacote fechado.
        /// </summary>         
        ///<response code="201">pacote fechado criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PacoteFechadoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarPacoteFechadoInputModel pacoteFechado)
        {
            var retorno = await _pacoteFechadoServico.Adicionar(_mapper.Map<PacoteFechado>(pacoteFechado));
            return Ok(_mapper.Map<PacoteFechadoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um pacote fechado.
        /// </summary>         
        ///<response code="200">pacote fechado atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PacoteFechadoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] PacoteFechadoInputModel pacoteFechado)
        {
            // Busca o registro existente
            var pacoteFechadoExistente = await _pacoteFechadoServico.BuscarPorID(pacoteFechado.PacoteFechadoId);
            if (pacoteFechadoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(pacoteFechado, pacoteFechadoExistente); // Faz o merge

            var retorno = await _pacoteFechadoServico.Atualizar(_mapper.Map<PacoteFechado>(pacoteFechadoExistente));
            return Ok(_mapper.Map<PacoteFechadoViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um pacote fechado.
        /// </summary>         
        ///<response code="200">pacote fechado excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int pacoteFechadoID)
        {
            await _pacoteFechadoServico.Deletar(pacoteFechadoID);
            return Ok();
        }
    }
}
