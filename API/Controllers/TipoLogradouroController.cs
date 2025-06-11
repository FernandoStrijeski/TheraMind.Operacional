using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.TiposLogradouros;
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
    public class TipoLogradouroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoLogradouroServico _tipoLogradouroServico;
        private readonly IHttpContextAccessor _httpContext;

        public TipoLogradouroController(
            IMapper mapper,
            ITipoLogradouroServico tipoLogradouroServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _tipoLogradouroServico = tipoLogradouroServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de logradouro a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o logradouro pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(TipoLogradouroViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(string tipoLogradouroID)
        {
            TipoLogradouro? tipoLogradouro = await _tipoLogradouroServico.BuscarPorID(tipoLogradouroID);
            if (tipoLogradouro == null)
                return NotFound("Nenhum tipo de logradouro encontrado");


            var resultado = _mapper.Map<TipoLogradouroViewModel>(tipoLogradouro);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de logradouros pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<TipoLogradouroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var tiposLogradouros = await _tipoLogradouroServico.BuscarPorNome(parametro);

            if (tiposLogradouros == null || tiposLogradouros.Count == 0)
                return NotFound("Nenhum tipo de logradouro encontrado");

            var resultado = _mapper.Map<List<TipoLogradouroViewModel>>(tiposLogradouros);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de logradouros
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<TipoLogradouroViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var tiposLogradouros = await _tipoLogradouroServico.BuscarTodos();

            if (tiposLogradouros == null || tiposLogradouros.Count == 0)
                return NotFound("Nenhum tipo de logradouro encontrado");


            var resultado = _mapper.Map<List<TipoLogradouroViewModel>>(tiposLogradouros);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um tipo de logradouro.
        /// </summary>         
        ///<response code="201">Tipo de logradouro criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoLogradouroViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarTipoLogradouroInputModel tipoLogradouro)
        {
            var retorno = await _tipoLogradouroServico.Adicionar(_mapper.Map<TipoLogradouro>(tipoLogradouro));
            return Ok(_mapper.Map<TipoLogradouroViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um tipo de logradouro.
        /// </summary>         
        ///<response code="200">Tipo de logradouro atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(TipoLogradouroViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] TipoLogradouroInputModel tipoLogradouro)
        {
            // Busca o registro existente
            var tipoLogradouroExistente = await _tipoLogradouroServico.BuscarPorID(tipoLogradouro.TipoLogradouroId);
            if (tipoLogradouroExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(tipoLogradouro, tipoLogradouroExistente); // Faz o merge

            var retorno = await _tipoLogradouroServico.Atualizar(_mapper.Map<TipoLogradouro>(tipoLogradouroExistente));
            return Ok(_mapper.Map<TipoLogradouroViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um tipo de logradouro.
        /// </summary>         
        ///<response code="200">Tipo de logradouro excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            await _tipoLogradouroServico.Deletar(id);
            return Ok();
        }
    }
}
