using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.EmpresaFaturas;
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
    public class EmpresaFaturaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaFaturaServico _empresaFaturaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaFaturaController(
            IMapper mapper,
            IEmpresaFaturaServico empresaFaturaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaFaturaServico = empresaFaturaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a fatura da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a fatura da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EmpresaFaturaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int empresaFaturaID)
        {
            EmpresaFatura? empresaFatura = await _empresaFaturaServico.BuscarPorID(empresaFaturaID);
            if (empresaFatura == null)
                return NotFound("Nenhuma fatura da empresa encontrada");


            var resultado = _mapper.Map<EmpresaFaturaViewModel>(empresaFatura);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as faturas da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EmpresaFaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var empresaFaturas = await _empresaFaturaServico.BuscarTodos();

            if (empresaFaturas == null || empresaFaturas.Count == 0)
                return NotFound("Nenhuma fatura da empresa encontrada");


            var resultado = _mapper.Map<List<EmpresaFaturaViewModel>>(empresaFaturas);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um fatura da empresa.
        /// </summary>         
        ///<response code="201">fatura da empresa criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaFaturaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEmpresaFaturaInputModel empresaFatura)
        {
            var retorno = await _empresaFaturaServico.Adicionar(_mapper.Map<EmpresaFatura>(empresaFatura));
            return Ok(_mapper.Map<EmpresaFaturaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma fatura da empresa.
        /// </summary>         
        ///<response code="200">Fatura da empresa atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaFaturaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EmpresaFaturaInputModel empresaFatura)
        {
            // Busca o registro existente
            var empresaFaturaExistente = await _empresaFaturaServico.BuscarPorID(empresaFatura.EmpresaFaturaId);
            if (empresaFaturaExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(empresaFatura, empresaFaturaExistente); // Faz o merge

            var retorno = await _empresaFaturaServico.Atualizar(_mapper.Map<EmpresaFatura>(empresaFaturaExistente));
            return Ok(_mapper.Map<EmpresaFaturaInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma fatura da empresa.
        /// </summary>         
        ///<response code="200">Fatura da empresa excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            await _empresaFaturaServico.Deletar(id);
            return Ok();
        }
    }
}
