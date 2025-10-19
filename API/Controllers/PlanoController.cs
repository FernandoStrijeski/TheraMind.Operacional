using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Planos;
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
    public class PlanoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlanoServico _planoServico;
        private readonly IHttpContextAccessor _httpContext;

        public PlanoController(
            IMapper mapper,
            IPlanoServico planoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _planoServico = planoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca plano a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o plano pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(PlanoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid planoID)
        {
            Plano? plano = await _planoServico.BuscarPorID(planoID);
            if (plano == null)
                return NotFound("Nenhum plano encontrado");


            var resultado = _mapper.Map<PlanoViewModel>(plano);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os planos pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<PlanoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var planos = await _planoServico.BuscarPorNome(parametro);

            if (planos == null || planos.Count == 0)
                return NotFound("Nenhum plano encontrado");

            var resultado = _mapper.Map<List<PlanoViewModel>>(planos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os planos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<PlanoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var planos = await _planoServico.BuscarTodos();

            if (planos == null || planos.Count == 0)
                return NotFound("Nenhum plano encontrado");


            var resultado = _mapper.Map<List<PlanoViewModel>>(planos);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um plano.
        /// </summary>         
        ///<response code="201">Plano criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PlanoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarPlanoInputModel plano)
        {
            var retorno = await _planoServico.Adicionar(_mapper.Map<Plano>(plano));
            return Ok(_mapper.Map<PlanoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um plano.
        /// </summary>         
        ///<response code="200">Plano atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(PlanoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] PlanoInputModel plano)
        {
            // Busca o registro existente
            var planoExistente = await _planoServico.BuscarPorID(plano.PlanoId);
            if (planoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(plano, planoExistente); // Faz o merge

            var retorno = await _planoServico.Atualizar(_mapper.Map<Plano>(planoExistente));
            return Ok(_mapper.Map<PlanoViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um plano.
        /// </summary>         
        ///<response code="200">Plano excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid planoID)
        {
            await _planoServico.Deletar(planoID);
            return Ok();
        }
    }
}
