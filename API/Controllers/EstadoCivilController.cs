using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.EstadosCivis;
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
    public class EstadoCivilController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEstadoCivilServico _estadoCivilServico;
        private readonly IHttpContextAccessor _httpContext;

        public EstadoCivilController(
            IMapper mapper,
            IEstadoCivilServico estadoCivilServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _estadoCivilServico = estadoCivilServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de estado civil a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de estado civil pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EstadoCivilViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string estadoCivilID)
        {
            EstadoCivil? estadoCivil = await _estadoCivilServico.BuscarPorID(estadoCivilID);
            if (estadoCivil == null)
                return NotFound("Nenhum tipo de estado civil encontrado");


            var resultado = _mapper.Map<EstadoCivilViewModel>(estadoCivil);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de estados civis pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<EstadoCivilViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var estadoCivil = await _estadoCivilServico.BuscarPorNome(parametro);

            if (estadoCivil == null || estadoCivil.Count == 0)
                return NotFound("Nenhum tipo de estado civil encontrado");

            var resultado = _mapper.Map<List<EstadoCivilViewModel>>(estadoCivil);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de estados civis
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EstadoCivilViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var estadoCivil = await _estadoCivilServico.BuscarTodos();

            if (estadoCivil == null || estadoCivil.Count == 0)
                return NotFound("Nenhum tipo de estado civil encontrado");


            var resultado = _mapper.Map<List<EstadoCivilViewModel>>(estadoCivil);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um estado civil.
        /// </summary>         
        ///<response code="201">Estado civil criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EstadoCivilViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEstadoCivilInputModel estadoCivil)
        {
            var retorno = await _estadoCivilServico.Adicionar(_mapper.Map<EstadoCivil>(estadoCivil));
            return Ok(_mapper.Map<EstadoCivilViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um estado civil.
        /// </summary>         
        ///<response code="200">Estado civil atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EstadoCivilViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EstadoCivilInputModel estadoCivil)
        {
            // Busca o registro existente
            var estadoCivilExistente = await _estadoCivilServico.BuscarPorID(estadoCivil.EstadoCivilId);
            if (estadoCivilExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(estadoCivil, estadoCivilExistente); // Faz o merge

            var retorno = await _estadoCivilServico.Atualizar(_mapper.Map<EstadoCivil>(estadoCivilExistente));
            return Ok(_mapper.Map<EstadoCivilInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um estado civil.
        /// </summary>         
        ///<response code="200">Estado civil excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] string estadoCivilID)
        {
            await _estadoCivilServico.Deletar(estadoCivilID);
            return Ok();
        }
    }
}
