using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.AnamneseSubGrupos;
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
    public class AnamneseSubGrupoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAnamneseSubGrupoServico _anamneseSubGrupoServico;
        private readonly IHttpContextAccessor _httpContext;

        public AnamneseSubGrupoController(
            IMapper mapper,
            IAnamneseSubGrupoServico anamneseSubGrupoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _anamneseSubGrupoServico = anamneseSubGrupoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca o subgrupo de anamnese a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o subgrupo de anamnese pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(AnamneseSubGrupoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int anamneseSubGrupoID)
        {
            AnamneseSubGrupo? anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarPorID(anamneseSubGrupoID);
            if (anamneseSubGrupo == null)
                return NotFound("Nenhum subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<AnamneseSubGrupoViewModel>(anamneseSubGrupo);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os subgrupos de anamnese pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarPorNome(parametro);

            if (anamneseSubGrupo == null || anamneseSubGrupo.Count == 0)
                return NotFound("Nenhum subgrupo de anamnese encontrado");

            var resultado = _mapper.Map<List<AnamneseSubGrupoViewModel>>(anamneseSubGrupo);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os subgrupos de anamnese
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<AnamneseSubGrupoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var anamneseSubGrupo = await _anamneseSubGrupoServico.BuscarTodos();

            if (anamneseSubGrupo == null || anamneseSubGrupo.Count == 0)
                return NotFound("Nenhum subgrupo de anamnese encontrado");


            var resultado = _mapper.Map<List<AnamneseSubGrupoViewModel>>(anamneseSubGrupo);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um subgrupo de anamnese.
        /// </summary>         
        ///<response code="201">Subgrupo de anamnese criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarAnamneseSubGrupoInputModel anamneseSubGrupo)
        {
            var retorno = await _anamneseSubGrupoServico.Adicionar(_mapper.Map<AnamneseSubGrupo>(anamneseSubGrupo));
            return Ok(_mapper.Map<AnamneseSubGrupoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um subgrupo de anamnese.
        /// </summary>         
        ///<response code="200">Subgrupo de anamnese atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(AnamneseSubGrupoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] AnamneseSubGrupoInputModel anamneseSubGrupo)
        {
            // Busca o registro existente
            var anamneseSubGrupoExistente = await _anamneseSubGrupoServico.BuscarPorID(anamneseSubGrupo.AnamneseSubGrupoId);
            if (anamneseSubGrupoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(anamneseSubGrupo, anamneseSubGrupoExistente); // Faz o merge

            var retorno = await _anamneseSubGrupoServico.Atualizar(_mapper.Map<AnamneseSubGrupo>(anamneseSubGrupoExistente));
            return Ok(_mapper.Map<AnamneseSubGrupoInputModel>(retorno));
        }

        /// <summary>
        /// Exclui um subgrupo de anamnese.
        /// </summary>         
        ///<response code="200">Subgrupo de anamnese excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _anamneseSubGrupoServico.Deletar(id);
            return Ok();
        }
    }
}
