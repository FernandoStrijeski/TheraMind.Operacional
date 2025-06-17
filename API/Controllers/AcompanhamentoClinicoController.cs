using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AcompanhamentosClinicos;
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
    public class AcompanhamentoClinicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAcompanhamentoClinicoServico _acompanhamentoClinicoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AcompanhamentoClinicoController(
            IMapper mapper,
            IAcompanhamentoClinicoServico acompanhamentoClinicoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _acompanhamentoClinicoServico = acompanhamentoClinicoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o acompanhamento clínico a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o acompanhamento clínico pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AcompanhamentoClinicoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid acompanhamentoClinicoID)
        {
            AcompanhamentoClinico? convenio = await _acompanhamentoClinicoServico.BuscarPorID(acompanhamentoClinicoID);
            if (convenio == null)
                return NotFound("Nenhum acompanhamento clínico encontrado");

            var resultado = _mapper.Map<AcompanhamentoClinicoViewModel>(convenio);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acompanhamentos clínicos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AcompanhamentoClinicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var acompanhamentosClinicos = await _acompanhamentoClinicoServico.BuscarTodos();

            if (acompanhamentosClinicos == null || acompanhamentosClinicos.Count == 0)
                return NotFound("Nenhum acompanhamento clínico encontrado");


            var resultado = _mapper.Map<List<AcompanhamentoClinicoViewModel>>(acompanhamentosClinicos);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acompanhamentos clínicos do Clientes e Profissional
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodosPorProfissionalCliente")]
        [ProducesResponseType(
            typeof(List<AcompanhamentoClinicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,PROFISSIONAL")]
        public async Task<ActionResult> BuscarTodosPorProfissionalCliente(Guid profissionalID, Guid clienteID)
        {
            var acompanhamentosClinicos = await _acompanhamentoClinicoServico.BuscarTodosPorProfissionalCliente(profissionalID, clienteID);

            if (acompanhamentosClinicos == null || acompanhamentosClinicos.Count == 0)
                return NotFound("Nenhum acompanhamento clínico encontrado");

            var resultado = _mapper.Map<List<AcompanhamentoClinicoViewModel>>(acompanhamentosClinicos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria um acompanhamento clínico.
        /// </summary>         
        ///<response code="201">Acompanhamento clínico criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AcompanhamentoClinicoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAcompanhamentoClinicoInputModel acompanhamentoClinico)
        {
            var retorno = await _acompanhamentoClinicoServico.Adicionar(_mapper.Map<AcompanhamentoClinico>(acompanhamentoClinico));
            return Ok(_mapper.Map<AcompanhamentoClinicoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um acompanhamento clínico.
        /// </summary>         
        ///<response code="200">Acompanhamento clínico atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AcompanhamentoClinicoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AcompanhamentoClinicoInputModel acompanhamentoClinico)
        {
            // Busca o registro existente
            var acompanhamentoClinicoExistente = await _acompanhamentoClinicoServico.BuscarPorID(acompanhamentoClinico.AcompanhamentoClinicoId);
            if (acompanhamentoClinicoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(acompanhamentoClinico, acompanhamentoClinicoExistente); // Faz o merge

            var retorno = await _acompanhamentoClinicoServico.Atualizar(_mapper.Map<AcompanhamentoClinico>(acompanhamentoClinicoExistente));
            return Ok(_mapper.Map<AcompanhamentoClinicoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um acompanhamento clínico.
        /// </summary>         
        ///<response code="200">Acompanhamento clínico excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid acompanhamentoClinicoID)
        {
            await _acompanhamentoClinicoServico.Deletar(acompanhamentoClinicoID);
            return Ok();
        }
    }
}
