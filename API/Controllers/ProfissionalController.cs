using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Profissionais;
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
    public class ProfissionalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProfissionalServico _profissionalServico;
        private readonly IHttpContextAccessor _httpContext;

        public ProfissionalController(
            IMapper mapper,
            IProfissionalServico profissionalServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _profissionalServico = profissionalServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o profissional a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o profissional pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ProfissionalViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid profissionalID)
        {
            Profissional? profissional = await _profissionalServico.BuscarPorID(profissionalID);
            if (profissional == null)
                return NotFound("Nenhum profissional encontrado");


            var resultado = _mapper.Map<ProfissionalViewModel>(profissional);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os profissionais pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var profissionais = await _profissionalServico.BuscarPorNome(parametro);

            if (profissionais == null || profissionais.Count == 0)
                return NotFound("Nenhum profissional encontrado");

            var resultado = _mapper.Map<List<ProfissionalViewModel>>(profissionais);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os profissionais
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ProfissionalViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var profissionais = await _profissionalServico.BuscarTodos();

            if (profissionais == null || profissionais.Count == 0)
                return NotFound("Nenhum profissional encontrado");


            var resultado = _mapper.Map<List<ProfissionalViewModel>>(profissionais);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um profissional.
        /// </summary>         
        ///<response code="201">Profissional criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarProfissionalInputModel profissional)
        {
            var retorno = await _profissionalServico.Adicionar(_mapper.Map<Profissional>(profissional));
            return Ok(_mapper.Map<ProfissionalViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um profissional.
        /// </summary>         
        ///<response code="200">Profissional atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ProfissionalViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ProfissionalInputModel profissional)
        {
            // Busca o registro existente
            var profissionalExistente = await _profissionalServico.BuscarPorID(profissional.ProfissionalId);
            if (profissionalExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(profissional, profissionalExistente); // Faz o merge

            var retorno = await _profissionalServico.Atualizar(_mapper.Map<Profissional>(profissionalExistente));
            return Ok(_mapper.Map<ProfissionalViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um profissional.
        /// </summary>         
        ///<response code="200">Profissional excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            await _profissionalServico.Deletar(id);
            return Ok();
        }
    }
}
