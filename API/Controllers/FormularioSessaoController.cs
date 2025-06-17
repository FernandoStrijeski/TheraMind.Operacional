using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.FormulariosSessoes;
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
    public class FormularioSessaoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFormularioSessaoServico _formularioSessaoServico;
        private readonly IHttpContextAccessor _httpContext;

        public FormularioSessaoController(
            IMapper mapper,
            IFormularioSessaoServico formularioSessaoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _formularioSessaoServico = formularioSessaoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o formulário de sessão a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o formulário de sessão pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(FormularioSessaoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int formularioSessaoID)
        {
            FormularioSessao? formularioSessao = await _formularioSessaoServico.BuscarPorID(formularioSessaoID);
            if (formularioSessao == null)
                return NotFound("Nenhum formulário de sessão encontrado");


            var resultado = _mapper.Map<FormularioSessaoViewModel>(formularioSessao);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os formulários de sessão pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var formularioSessoes = await _formularioSessaoServico.BuscarPorNome(parametro);

            if (formularioSessoes == null || formularioSessoes.Count == 0)
                return NotFound("Nenhum formulário de sessão encontrado");

            var resultado = _mapper.Map<List<FormularioSessaoViewModel>>(formularioSessoes);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os formulários de sessão
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var formularioSessoes = await _formularioSessaoServico.BuscarTodos();

            if (formularioSessoes == null || formularioSessoes.Count == 0)
                return NotFound("Nenhum formulário de sessão encontrado");


            var resultado = _mapper.Map<List<FormularioSessaoViewModel>>(formularioSessoes);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria um formulário de sessão.
        /// </summary>         
        ///<response code="201">Formulário de sessão criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FormularioSessaoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarFormularioSessaoInputModel formularioSessao)
        {
            var retorno = await _formularioSessaoServico.Adicionar(_mapper.Map<FormularioSessao>(formularioSessao));
            return Ok(_mapper.Map<FormularioSessaoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um formulário de sessão.
        /// </summary>         
        ///<response code="200">Formulário de sessão atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FormularioSessaoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] FormularioSessaoInputModel formularioSessao)
        {
            // Busca o registro existente
            var formularioSessaoExistente = await _formularioSessaoServico.BuscarPorID(formularioSessao.FormularioSessaoId);
            if (formularioSessaoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(formularioSessao, formularioSessaoExistente); // Faz o merge

            var retorno = await _formularioSessaoServico.Atualizar(_mapper.Map<FormularioSessao>(formularioSessaoExistente));
            return Ok(_mapper.Map<FormularioSessaoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um formulário de sessão.
        /// </summary>         
        ///<response code="200">Formulário de sessão excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int formularioSessaoID)
        {
            await _formularioSessaoServico.Deletar(formularioSessaoID);
            return Ok();
        }
    }
}
