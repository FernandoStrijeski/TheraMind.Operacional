using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Paises;
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
    public class PaisController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPaisServico _paisServico;
        private readonly IHttpContextAccessor _httpContext;

        public PaisController(
            IMapper mapper,
            IPaisServico paisServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _paisServico = paisServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o país a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o país pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(PaisViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int paisID)
        {
            Pais? pais = await _paisServico.BuscarPorID(paisID);
            if (pais == null)
                return NotFound("Nenhum país encontrado");


            var resultado = _mapper.Map<PaisViewModel>(pais);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os países pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<PaisViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var pais = await _paisServico.BuscarPorNome(parametro);

            if (pais == null || pais.Count == 0)
                return NotFound("Nenhum país encontrado");

            var resultado = _mapper.Map<List<PaisViewModel>>(pais);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os países
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<PaisViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var paises = await _paisServico.BuscarTodos();

            if (paises == null || paises.Count == 0)
                return NotFound("Nenhum país encontrado");


            var resultado = _mapper.Map<List<PaisViewModel>>(paises);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um país.
        /// </summary>         
        ///<response code="201">País criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PaisViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarPaisInputModel pais)
        {
            var retorno = await _paisServico.Adicionar(_mapper.Map<Pais>(pais));
            return Ok(_mapper.Map<PaisViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um país.
        /// </summary>         
        ///<response code="200">País atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PaisViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] PaisInputModel pais)
        {
            // Busca o registro existente
            var paisExistente = await _paisServico.BuscarPorID(pais.PaisId);
            if (paisExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(pais, paisExistente); // Faz o merge

            var retorno = await _paisServico.Atualizar(_mapper.Map<Pais>(paisExistente));
            return Ok(_mapper.Map<PaisViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um país.
        /// </summary>         
        ///<response code="200">País excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _paisServico.Deletar(id);
            return Ok();
        }
    }
}
