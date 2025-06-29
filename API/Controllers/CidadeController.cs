using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Cidades;
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
    public class CidadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICidadeServico _cidadeServico;
        private readonly IHttpContextAccessor _httpContext;

        public CidadeController(
            IMapper mapper,
            ICidadeServico cidadeServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _cidadeServico = cidadeServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a cidade a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a cidade pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(CidadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int cidadeID)
        {
            Cidade? cidade = await _cidadeServico.BuscarPorID(cidadeID);
            if (cidade == null)
                return NotFound("Nenhuma cidade encontrada");


            var resultado = _mapper.Map<CidadeViewModel>(cidade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca as cidades pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<CidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var cidade = await _cidadeServico.BuscarPorNome(parametro);

            if (cidade == null || cidade.Count == 0)
                return NotFound("Nenhuma cidade encontrada");

            var resultado = _mapper.Map<List<CidadeViewModel>>(cidade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca a cidade a partir do código IBGE
        /// </summary>
        /// <response code="200">Retorna a cidade pelo código do IBGE informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorIBGE")]
        [ProducesResponseType(
            typeof(CidadeViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorIBGE(int codigoIBGE)
        {
            Cidade? cidade = await _cidadeServico.BuscarPorIBGE(codigoIBGE);
            if (cidade == null)
                return NotFound("Nenhuma cidade encontrada");


            var resultado = _mapper.Map<CidadeViewModel>(cidade);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as cidades do Estado
        /// </summary>
        /// <param name="UF"></param>
        /// <returns></returns>
        [HttpGet("ObterPorEstado")]
        [ProducesResponseType(
            typeof(List<CidadeViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodasPorEstado([FromQuery] string UF)
        {
            var cidades = await _cidadeServico.BuscarTodasPorEstado(UF);

            if (cidades == null || cidades.Count == 0)
                return NotFound("Nenhuma cidade encontrada");

            var resultado = _mapper.Map<List<CidadeViewModel>>(cidades);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria uma cidade.
        /// </summary>         
        ///<response code="201">Cidade criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(CidadeViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarCidadeInputModel cidade)
        {
            var retorno = await _cidadeServico.Adicionar(_mapper.Map<Cidade>(cidade));
            return Ok(_mapper.Map<CidadeViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma cidade.
        /// </summary>         
        ///<response code="200">Cidade atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(CidadeViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] CidadeInputModel cidade)
        {
            // Busca o registro existente
            var cidadeExistente = await _cidadeServico.BuscarPorID(cidade.CidadeId);
            if (cidadeExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(cidade, cidadeExistente); // Faz o merge

            var retorno = await _cidadeServico.Atualizar(_mapper.Map<Cidade>(cidadeExistente));
            return Ok(_mapper.Map<CidadeInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma cidade.
        /// </summary>         
        ///<response code="200">Cidade excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int cidadeID)
        {
            await _cidadeServico.Deletar(cidadeID);
            return Ok();
        }
    }
}
