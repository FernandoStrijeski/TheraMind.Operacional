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
        [HttpGet("ObterPorId")]
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
        [HttpGet("ObterPorEmail")]
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
        [HttpGet("ObterTodos")]
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
        /// Cria um usuário.
        /// </summary>         
        ///<response code="201">Usuário criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarUsuarioInputModel usuario)
        {
            var retorno = await _usuarioServico.Adicionar(_mapper.Map<Usuario>(usuario));
            return Ok(_mapper.Map<UsuarioViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um usuário.
        /// </summary>         
        ///<response code="200">Usuário atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] UsuarioInputModel usuario)
        {
            // Busca o registro existente
            var usuarioExistente = await _usuarioServico.BuscarPorID(usuario.UsuarioId);
            if (usuarioExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(usuario, usuarioExistente); // Faz o merge

            var retorno = await _usuarioServico.Atualizar(_mapper.Map<Usuario>(usuarioExistente));
            return Ok(_mapper.Map<UsuarioViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um usuário.
        /// </summary>         
        ///<response code="200">Usuário excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            await _usuarioServico.Deletar(id);
            return Ok();
        }
    }
}
