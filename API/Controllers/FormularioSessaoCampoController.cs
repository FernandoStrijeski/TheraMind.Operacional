using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.FormularioSessaoCampos;
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
    public class FormularioSessaoCampoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFormularioSessaoCampoServico _formularioSessaoCampoServico;
        private readonly IHttpContextAccessor _httpContext;

        public FormularioSessaoCampoController(
            IMapper mapper,
            IFormularioSessaoCampoServico formularioSessaoCampoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _formularioSessaoCampoServico = formularioSessaoCampoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o campo do formulário de sessão a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o formulário de sessão pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(FormularioSessaoCampoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int formularioSessaoCampoID)
        {
            FormularioSessaoCampo? formularioSessaoCampo = await _formularioSessaoCampoServico.BuscarPorID(formularioSessaoCampoID);
            if (formularioSessaoCampo == null)
                return NotFound("Nenhum campo do formulário de sessão encontrado");


            var resultado = _mapper.Map<FormularioSessaoCampoViewModel>(formularioSessaoCampo);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os campos dos formulários de sessão pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoCampoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var formularioSessaoCampos = await _formularioSessaoCampoServico.BuscarPorNome(parametro);

            if (formularioSessaoCampos == null || formularioSessaoCampos.Count == 0)
                return NotFound("Nenhum campo do formulário de sessão encontrado");

            var resultado = _mapper.Map<List<FormularioSessaoCampoViewModel>>(formularioSessaoCampos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os campos dos formulários de sessão
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<FormularioSessaoCampoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var formularioSessaoCampos = await _formularioSessaoCampoServico.BuscarTodos();

            if (formularioSessaoCampos == null || formularioSessaoCampos.Count == 0)
                return NotFound("Nenhum campo do formulário de sessão encontrado");


            var resultado = _mapper.Map<List<FormularioSessaoCampoViewModel>>(formularioSessaoCampos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria um campo do formulário de sessão.
        /// </summary>         
        ///<response code="201">Campo do formulário de sessão criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FormularioSessaoCampoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarFormularioSessaoCampoInputModel formularioSessaoCampo)
        {
            var retorno = await _formularioSessaoCampoServico.Adicionar(_mapper.Map<FormularioSessaoCampo>(formularioSessaoCampo));
            return Ok(_mapper.Map<FormularioSessaoCampoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um campo do formulário de sessão.
        /// </summary>         
        ///<response code="200">Campo do formulário de sessão atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FormularioSessaoCampoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] FormularioSessaoCampoInputModel formularioSessaoCampo)
        {
            // Busca o registro existente
            var formularioSessaoCampoExistente = await _formularioSessaoCampoServico.BuscarPorID(formularioSessaoCampo.FormularioSessaoId);
            if (formularioSessaoCampoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(formularioSessaoCampo, formularioSessaoCampoExistente); // Faz o merge

            var retorno = await _formularioSessaoCampoServico.Atualizar(_mapper.Map<FormularioSessaoCampo>(formularioSessaoCampoExistente));
            return Ok(_mapper.Map<FormularioSessaoCampoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um campo do formulário de sessão.
        /// </summary>         
        ///<response code="200">Campo do formulário de sessão excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int formularioSessaoCampoID)
        {
            await _formularioSessaoCampoServico.Deletar(formularioSessaoCampoID);
            return Ok();
        }
    }
}
