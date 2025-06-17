using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.OrientacoesSexuais;
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
    public class OrientacaoSexualController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrientacaoSexualServico _orientacaoSexualServico;
        private readonly IHttpContextAccessor _httpContext;

        public OrientacaoSexualController(
            IMapper mapper,
            IOrientacaoSexualServico orientacaoSexualServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _orientacaoSexualServico = orientacaoSexualServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o tipo de orientação sexual a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o tipo de orientação sexual pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(OrientacaoSexualViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int orientacaoSexualID)
        {
            OrientacaoSexual? orientacaoSexual = await _orientacaoSexualServico.BuscarPorID(orientacaoSexualID);
            if (orientacaoSexual == null)
                return NotFound("Nenhum tipo de orientação sexual encontrado");


            var resultado = _mapper.Map<OrientacaoSexualViewModel>(orientacaoSexual);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os tipos de orientações sexuais pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<OrientacaoSexualViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var orientacaoSexual = await _orientacaoSexualServico.BuscarPorNome(parametro);

            if (orientacaoSexual == null || orientacaoSexual.Count == 0)
                return NotFound("Nenhum tipo de orientação sexual encontrado");

            var resultado = _mapper.Map<List<OrientacaoSexualViewModel>>(orientacaoSexual);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os tipos de orientações sexuais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<OrientacaoSexualViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarTodos()
        {
            var orientacaoSexual = await _orientacaoSexualServico.BuscarTodos();

            if (orientacaoSexual == null || orientacaoSexual.Count == 0)
                return NotFound("Nenhum tipo de orientação sexual encontrado");


            var resultado = _mapper.Map<List<OrientacaoSexualViewModel>>(orientacaoSexual);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria uma orientação sexual.
        /// </summary>         
        ///<response code="201">Orientação sexual criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(OrientacaoSexualViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarOrientacaoSexualInputModel orientacaoSexual)
        {
            var retorno = await _orientacaoSexualServico.Adicionar(_mapper.Map<OrientacaoSexual>(orientacaoSexual));
            return Ok(_mapper.Map<OrientacaoSexualViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma orientação sexual.
        /// </summary>         
        ///<response code="200">Orientação sexual atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(OrientacaoSexualViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] OrientacaoSexualInputModel orientacaoSexual)
        {
            // Busca o registro existente
            var orientacaoSexualExistente = await _orientacaoSexualServico.BuscarPorID(orientacaoSexual.OrientacaoSexualId);
            if (orientacaoSexualExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(orientacaoSexual, orientacaoSexualExistente); // Faz o merge

            var retorno = await _orientacaoSexualServico.Atualizar(_mapper.Map<OrientacaoSexual>(orientacaoSexualExistente));
            return Ok(_mapper.Map<OrientacaoSexualInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma orientação sexual.
        /// </summary>         
        ///<response code="200">Orientação sexual excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int orientacaoSexualID)
        {
            await _orientacaoSexualServico.Deletar(orientacaoSexualID);
            return Ok();
        }
    }
}
