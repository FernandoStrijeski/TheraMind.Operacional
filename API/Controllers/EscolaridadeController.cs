using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Escolaridades;
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
    public class EscolaridadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEscolaridadeServico _escolaridadeServico;
        private readonly IHttpContextAccessor _httpContext;

        public EscolaridadeController(
            IMapper mapper,
            IEscolaridadeServico escolaridadeServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _escolaridadeServico = escolaridadeServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de escolaridade a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de escolaridade pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EscolaridadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int escolaridadeID)
        {
            Escolaridade? escolaridade = await _escolaridadeServico.BuscarPorID(escolaridadeID);
            if (escolaridade == null)
                return NotFound("Nenhuma escolaridade encontrada");


            var resultado = _mapper.Map<EscolaridadeViewModel>(escolaridade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de escolaridades pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<EscolaridadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var escolaridade = await _escolaridadeServico.BuscarPorNome(parametro);

            if (escolaridade == null || escolaridade.Count == 0)
                return NotFound("Nenhuma escolaridade encontrada");

            var resultado = _mapper.Map<List<EscolaridadeViewModel>>(escolaridade);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de escolaridades
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EscolaridadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var escolaridade = await _escolaridadeServico.BuscarTodos();

            if (escolaridade == null || escolaridade.Count == 0)
                return NotFound("Nenhuma escolaridade encontrada");


            var resultado = _mapper.Map<List<EscolaridadeViewModel>>(escolaridade);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria uma escolaridade.
        /// </summary>         
        ///<response code="201">Escolaridade criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EscolaridadeViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEscolaridadeInputModel escolaridade)
        {
            var retorno = await _escolaridadeServico.Adicionar(_mapper.Map<Escolaridade>(escolaridade));
            return Ok(_mapper.Map<EscolaridadeViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma escolaridade.
        /// </summary>         
        ///<response code="200">Escolaridade atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EscolaridadeViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EscolaridadeInputModel escolaridade)
        {
            // Busca o registro existente
            var escolaridadeExistente = await _escolaridadeServico.BuscarPorID(escolaridade.EscolaridadeId);
            if (escolaridadeExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(escolaridade, escolaridadeExistente); // Faz o merge

            var retorno = await _escolaridadeServico.Atualizar(_mapper.Map<Escolaridade>(escolaridadeExistente));
            return Ok(_mapper.Map<EscolaridadeInputModel>(retorno));            
        }

        /// <summary>
        /// Exclui uma escolaridade.
        /// </summary>         
        ///<response code="200">Escolaridade excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _escolaridadeServico.Deletar(id);
            return Ok();
        }
    }
}
