using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.ProfissionaisAcessos;
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
    public class ProfissionalAcessoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfissionalAcessoServico _profissionalAcessoServico;
        private readonly IHttpContextAccessor _httpContext;

        public ProfissionalAcessoController(
            IMapper mapper,
            IProfissionalAcessoServico profissionalAcessoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _profissionalAcessoServico = profissionalAcessoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o acesso profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o acesso do profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ProfissionalAcessoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int profissionalAcessoID)
        {
            ProfissionalAcesso? profissionalAcesso = await _profissionalAcessoServico.BuscarPorID(profissionalAcessoID);
            if (profissionalAcesso == null)
                return NotFound("Nenhum acesso profissional encontrado");


            var resultado = _mapper.Map<ProfissionalAcessoViewModel>(profissionalAcesso);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca o acesso profissional a partir do identificador do profissional informado
        /// </summary>
        /// <response code="200">Retorna o acesso do profissional pelo ID do profissional informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorIdProfissional")]
        [ProducesResponseType(
            typeof(ProfissionalAcessoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorIDProfissional(Guid profissionalID)
        {
            var acessosprofissionais = await _profissionalAcessoServico.BuscarPorIDProfissional(profissionalID);

            if (acessosprofissionais == null || acessosprofissionais.Count == 0)
                return NotFound("Nenhum acesso para o profissional encontrado");

            var resultado = _mapper.Map<List<ProfissionalAcessoViewModel>>(acessosprofissionais);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todos os acessos profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ProfissionalAcessoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var acessosprofissionais = await _profissionalAcessoServico.BuscarTodos();

            if (acessosprofissionais == null || acessosprofissionais.Count == 0)
                return NotFound("Nenhum acesso para o profissional encontrado");


            var resultado = _mapper.Map<List<ProfissionalAcessoViewModel>>(acessosprofissionais);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um acesso para o profissional.
        /// </summary>         
        ///<response code="201">Acesso para o profissional criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalAcessoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarProfissionalAcessoInputModel profissionalAcesso)
        {
            var retorno = await _profissionalAcessoServico.Adicionar(_mapper.Map<ProfissionalAcesso>(profissionalAcesso));
            return Ok(_mapper.Map<ProfissionalAcessoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um acesso para o profissional.
        /// </summary>         
        ///<response code="200">Acesso para o profissional atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalAcessoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ProfissionalAcessoInputModel profissionalAcesso)
        {
            // Busca o registro existente
            var profissionalAcessoExistente = await _profissionalAcessoServico.BuscarPorID(profissionalAcesso.ProfissionalAcessoId);
            if (profissionalAcessoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(profissionalAcesso, profissionalAcessoExistente); // Faz o merge

            var retorno = await _profissionalAcessoServico.Atualizar(_mapper.Map<ProfissionalAcesso>(profissionalAcessoExistente));
            return Ok(_mapper.Map<ProfissionalAcessoViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um acesso para o profissional.
        /// </summary>         
        ///<response code="200">Acesso para o profissional excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int profissionalAcessoID)
        {
            await _profissionalAcessoServico.Deletar(profissionalAcessoID);
            return Ok();
        }
    }
}
