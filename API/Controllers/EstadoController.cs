using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Estados;
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
    public class EstadoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEstadoServico _estadoServico;
        private readonly IHttpContextAccessor _httpContext;

        public EstadoController(
            IMapper mapper,
            IEstadoServico estadoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _estadoServico = estadoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o estado a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o estado pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EstadoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string estadoID)
        {
            Estado? estado = await _estadoServico.BuscarPorID(estadoID);
            if (estado == null)
                return NotFound("Nenhum estado encontrado");


            var resultado = _mapper.Map<EstadoViewModel>(estado);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os estados pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<EstadoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var estado = await _estadoServico.BuscarPorNome(parametro);

            if (estado == null || estado.Count == 0)
                return NotFound("Nenhum estado encontrado");

            var resultado = _mapper.Map<List<EstadoViewModel>>(estado);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um estado.
        /// </summary>         
        ///<response code="201">Estado criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EstadoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEstadoInputModel estado)
        {
            var retorno = await _estadoServico.Adicionar(_mapper.Map<Estado>(estado));
            return Ok(_mapper.Map<EstadoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um estado.
        /// </summary>         
        ///<response code="200">Estado atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EstadoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EstadoInputModel estado)
        {
            // Busca o registro existente
            var estadoExistente = await _estadoServico.BuscarPorID(estado.UF);
            if (estadoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(estado, estadoExistente); // Faz o merge

            var retorno = await _estadoServico.Atualizar(_mapper.Map<Estado>(estadoExistente));
            return Ok(_mapper.Map<EstadoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um estado.
        /// </summary>         
        ///<response code="200">Estado excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            await _estadoServico.Deletar(id);
            return Ok();
        }
    }
}
