using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.IdentidadesGeneros;
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
    public class IdentidadeGeneroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentidadeGeneroServico _identidadeGeneroServico;
        private readonly IHttpContextAccessor _httpContext;

        public IdentidadeGeneroController(
            IMapper mapper,
            IIdentidadeGeneroServico identidadeGeneroServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _identidadeGeneroServico = identidadeGeneroServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de identidade de gênero a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de identidade de gênero pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(IdentidadeGeneroViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int identidadeGeneroID)
        {
            IdentidadeGenero? identidadeGenero = await _identidadeGeneroServico.BuscarPorID(identidadeGeneroID);
            if (identidadeGenero == null)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");


            var resultado = _mapper.Map<IdentidadeGeneroViewModel>(identidadeGenero);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de identidades de gêneros pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<IdentidadeGeneroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var identidadeGenero = await _identidadeGeneroServico.BuscarPorNome(parametro);

            if (identidadeGenero == null || identidadeGenero.Count == 0)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");

            var resultado = _mapper.Map<List<IdentidadeGeneroViewModel>>(identidadeGenero);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de identidades de gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<IdentidadeGeneroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var identidadesGeneros = await _identidadeGeneroServico.BuscarTodos();

            if (identidadesGeneros == null || identidadesGeneros.Count == 0)
                return NotFound("Nenhum tipo de identidade de gênero encontrado");


            var resultado = _mapper.Map<List<IdentidadeGeneroViewModel>>(identidadesGeneros);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma identidade de gênero.
        /// </summary>         
        ///<response code="201">Identidade de gênero criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(IdentidadeGeneroViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarIdentidadeGeneroInputModel identidadeGenero)
        {
            var retorno = await _identidadeGeneroServico.Adicionar(_mapper.Map<IdentidadeGenero>(identidadeGenero));
            return Ok(_mapper.Map<IdentidadeGeneroViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma identidade de gênero.
        /// </summary>         
        ///<response code="200">Identidade de gênero atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(IdentidadeGeneroViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] IdentidadeGeneroInputModel identidadeGenero)
        {
            // Busca o registro existente
            var identidadeGeneroExistente = await _identidadeGeneroServico.BuscarPorID(identidadeGenero.IdentidadeGeneroId);
            if (identidadeGeneroExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(identidadeGenero, identidadeGeneroExistente); // Faz o merge

            var retorno = await _identidadeGeneroServico.Atualizar(_mapper.Map<IdentidadeGenero>(identidadeGeneroExistente));
            return Ok(_mapper.Map<IdentidadeGeneroInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma identidade de gênero.
        /// </summary>         
        ///<response code="200">Identidade de gênero excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int identidadeGeneroID)
        {
            await _identidadeGeneroServico.Deletar(identidadeGeneroID);
            return Ok();
        }
    }
}
