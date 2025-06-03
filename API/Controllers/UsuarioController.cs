using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Usuarios;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioServico _usuarioServico;
        private readonly IHttpContextAccessor _httpContext;

        public UsuarioController(
            IMapper mapper,
            IUsuarioServico planoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _usuarioServico = planoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o usuário a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o usuário pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("BuscarPorID")]
        [ProducesResponseType(
            typeof(UsuarioViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid usuarioID)
        {
            Usuario? usuario = await _usuarioServico.BuscarPorID(usuarioID);
            if (usuario == null)
                return NotFound("Nenhum usuário encontrado");


            var resultado = _mapper.Map<UsuarioViewModel>(usuario);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os usuários pelo email
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("BuscarPorEmail")]
        [ProducesResponseType(
            typeof(List<UsuarioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorEmail([FromQuery] BuscarComEmailParametro parametro)
        {
            var usuarios = await _usuarioServico.BuscarPorEmail(parametro);

            if (usuarios == null || usuarios.Count == 0)
                return NotFound("Nenhum usuário encontrado");

            var resultado = _mapper.Map<List<UsuarioViewModel>>(usuarios);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("Todos")]
        [ProducesResponseType(
            typeof(List<UsuarioViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var planos = await _usuarioServico.BuscarTodos();

            if (planos == null || planos.Count == 0)
                return NotFound("Nenhum usuário encontrado");

            var resultado = _mapper.Map<List<UsuarioViewModel>>(planos);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria ou atualiza um usuário
        /// </summary>
        /// <response code="202">Usuário criado com sucesso. O corpo da resposta contém o ID gerado.</response>
        /// <response code="204">Usuário atualizado com sucesso</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPut("")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(UsuarioIdResponseViewModel), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Put([FromBody] CriarUsuarioInputModel body)
        {
            var (criou, usuarioId) = await _usuarioServico.CriarOuAtualizar(body, true);

            if (criou)
                return Accepted(new UsuarioIdResponseViewModel(usuarioId));

            return NoContent(); // Atualizado com sucesso, sem corpo            
        }
    }
}
