using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.GrauParentescos;
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
    public class GrauParentescoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGrauParentescoServico _grauParentescoServico;
        private readonly IHttpContextAccessor _httpContext;

        public GrauParentescoController(
            IMapper mapper,
            IGrauParentescoServico grauParentescoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _grauParentescoServico = grauParentescoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o grau de parentesco a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o grau de parentesco pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(GrauParentescoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int grauParentescoID)
        {
            GrauParentesco? grauParentesco = await _grauParentescoServico.BuscarPorID(grauParentescoID);
            if (grauParentesco == null)
                return NotFound("Nenhum grau de parentesco encontrado");

            var resultado = _mapper.Map<GrauParentescoViewModel>(grauParentesco);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os graus de parentescos pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<GrauParentescoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var grauParentesco = await _grauParentescoServico.BuscarPorNome(parametro);

            if (grauParentesco == null || grauParentesco.Count == 0)
                return NotFound("Nenhum grau de parentesco encontrado");

            var resultado = _mapper.Map<List<GrauParentescoViewModel>>(grauParentesco);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os graus de parentescos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<GrauParentescoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var grauParentesco = await _grauParentescoServico.BuscarTodos();

            if (grauParentesco == null || grauParentesco.Count == 0)
                return NotFound("Nenhum grau de parentesco encontrado");

            var resultado = _mapper.Map<List<GrauParentescoViewModel>>(grauParentesco);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um grau de parentesco.
        /// </summary>         
        ///<response code="201">Grau de parentesco criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrauParentescoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarGrauParentescoInputModel grauParentesco)
        {
            var retorno = await _grauParentescoServico.Adicionar(_mapper.Map<GrauParentesco>(grauParentesco));
            return Ok(_mapper.Map<GrauParentescoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um grau de parentesco.
        /// </summary>         
        ///<response code="200">Grau de parentesco atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(GrauParentescoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] GrauParentescoInputModel grauParentesco)
        {
            // Busca o registro existente
            var grauParentescoExistente = await _grauParentescoServico.BuscarPorID(grauParentesco.GrauParentescoId);
            if (grauParentescoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(grauParentesco, grauParentescoExistente); // Faz o merge

            var retorno = await _grauParentescoServico.Atualizar(_mapper.Map<GrauParentesco>(grauParentescoExistente));
            return Ok(_mapper.Map<GrauParentescoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um grau de parentesco.
        /// </summary>         
        ///<response code="200">Grau de parentesco excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int grauParentescoID)
        {
            await _grauParentescoServico.Deletar(grauParentescoID);
            return Ok();
        }
    }
}
