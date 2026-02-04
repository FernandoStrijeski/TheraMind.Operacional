using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Convenios;
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
    public class ConvenioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConvenioServico _convenioServico;
        private readonly IHttpContextAccessor _httpContext;

        public ConvenioController(
            IMapper mapper,
            IConvenioServico convenioServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _convenioServico = convenioServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o convênio a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o convênio pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ConvenioViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int convenioID)
        {
            Convenio? convenio = await _convenioServico.BuscarPorID(convenioID);
            if (convenio == null)
                return NotFound("Nenhum convênio encontrado");


            var resultado = _mapper.Map<ConvenioViewModel>(convenio);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os convênios pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ConvenioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var convenios = await _convenioServico.BuscarPorNome(parametro);

            if (convenios == null || convenios.Count == 0)
                return NotFound("Nenhum convênio encontrado");

            var resultado = _mapper.Map<List<ConvenioViewModel>>(convenios);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os convênios
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ConvenioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var convenios = await _convenioServico.BuscarTodos();

            if (convenios == null || convenios.Count == 0)
                return NotFound("Nenhum convênio encontrado");


            var resultado = _mapper.Map<List<ConvenioViewModel>>(convenios);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um convênio.
        /// </summary>         
        ///<response code="201">Convênio criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ConvenioViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarConvenioInputModel convenio)
        {
            var retorno = await _convenioServico.Adicionar(_mapper.Map<Convenio>(convenio));
            return Ok(_mapper.Map<ConvenioViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um convênio.
        /// </summary>         
        ///<response code="200">Convênio atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ConvenioViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ConvenioInputModel convenio)
        {
            // Busca o registro existente
            var convenioExistente = await _convenioServico.BuscarPorID(convenio.ConvenioId);
            if (convenioExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(convenio, convenioExistente); // Faz o merge

            var retorno = await _convenioServico.Atualizar(_mapper.Map<Convenio>(convenioExistente));
            return Ok(_mapper.Map<ConvenioInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um convênio.
        /// </summary>         
        ///<response code="200">Convênio excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int convenioID)
        {
            await _convenioServico.Deletar(convenioID);
            return Ok();
        }
    }
}
