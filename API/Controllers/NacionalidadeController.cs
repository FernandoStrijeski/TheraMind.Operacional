using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Nacionalidades;
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
    public class NacionalidadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INacionalidadeServico _nacionalidadeServico;
        private readonly IHttpContextAccessor _httpContext;

        public NacionalidadeController(
            IMapper mapper,
            INacionalidadeServico nacionalidadeServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _nacionalidadeServico = nacionalidadeServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a nacionalidade a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a nacionalidade pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(NacionalidadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int nacionalidadeID)
        {
            Nacionalidade? nacionalidade = await _nacionalidadeServico.BuscarPorID(nacionalidadeID);
            if (nacionalidade == null)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<NacionalidadeViewModel>(nacionalidade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as nacionalidades pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<NacionalidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var nacionalidade = await _nacionalidadeServico.BuscarPorNome(parametro);

            if (nacionalidade == null || nacionalidade.Count == 0)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<List<NacionalidadeViewModel>>(nacionalidade);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as nacionalidade
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<NacionalidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var nacionalidade = await _nacionalidadeServico.BuscarTodos();

            if (nacionalidade == null || nacionalidade.Count == 0)
                return NotFound("Nenhuma nacionalidade encontrada");

            var resultado = _mapper.Map<List<NacionalidadeViewModel>>(nacionalidade);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma nacionalidade.
        /// </summary>         
        ///<response code="201">Nacionalidade criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(NacionalidadeViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarNacionalidadeInputModel nacionalidade)
        {
            var retorno = await _nacionalidadeServico.Adicionar(_mapper.Map<Nacionalidade>(nacionalidade));
            return Ok(_mapper.Map<NacionalidadeViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma nacionalidade.
        /// </summary>         
        ///<response code="200">Nacionalidade atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(NacionalidadeViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] NacionalidadeInputModel nacionalidade)
        {
            // Busca o registro existente
            var nacionalidadeExistente = await _nacionalidadeServico.BuscarPorID(nacionalidade.NacionalidadeId);
            if (nacionalidadeExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(nacionalidade, nacionalidadeExistente); // Faz o merge

            var retorno = await _nacionalidadeServico.Atualizar(_mapper.Map<Nacionalidade>(nacionalidadeExistente));
            return Ok(_mapper.Map<NacionalidadeInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma nacionalidade.
        /// </summary>         
        ///<response code="200">Nacionalidade excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int nacionalidadeID)
        {
            await _nacionalidadeServico.Deletar(nacionalidadeID);
            return Ok();
        }
    }
}
