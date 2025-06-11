using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.EmpresasAssinaturas;
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
    public class EmpresaAssinaturaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaAssinaturaServico _empresaAssinaturaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaAssinaturaController(
            IMapper mapper,
            IEmpresaAssinaturaServico empresaAssinaturaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaAssinaturaServico = empresaAssinaturaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a assinatura da empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a assinatura da empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(EmpresaAssinaturaViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(Guid empresaAssinaturaID)
        {
            EmpresaAssinatura? empresaAssinatura = await _empresaAssinaturaServico.BuscarPorID(empresaAssinaturaID);
            if (empresaAssinatura == null)
                return NotFound("Nenhuma assinatura de empresa encontrada");


            var resultado = _mapper.Map<EmpresaAssinaturaViewModel>(empresaAssinatura);
            return Ok(resultado);
        }



        /// <summary>
        /// Busca todas as assinaturas das empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EmpresaAssinaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var empresasAssinaturas = await _empresaAssinaturaServico.BuscarTodos();

            if (empresasAssinaturas == null || empresasAssinaturas.Count == 0)
                return NotFound("Nenhuma assinatura de empresa encontrada");

            var resultado = _mapper.Map<List<EmpresaAssinaturaViewModel>>(empresasAssinaturas);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as assinaturas da empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterPorIdEmpresa")]
        [ProducesResponseType(
            typeof(List<EmpresaAssinaturaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorIdEmpresa(Guid empresaID)
        {
            var empresasAssinaturas = await _empresaAssinaturaServico.BuscarPorIdEmpresa(empresaID);

            if (empresasAssinaturas == null || empresasAssinaturas.Count == 0)
                return NotFound("Nenhuma assinatura de empresa encontrada");

            var resultado = _mapper.Map<List<EmpresaAssinaturaViewModel>>(empresasAssinaturas);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma assinatura de empresa.
        /// </summary>         
        ///<response code="201">Assinatura de empresa criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaAssinaturaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEmpresaAssinaturaInputModel empresaAssinatura)
        {
            var retorno = await _empresaAssinaturaServico.Adicionar(_mapper.Map<EmpresaAssinatura>(empresaAssinatura));
            return Ok(_mapper.Map<EmpresaAssinaturaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma assinatura de empresa.
        /// </summary>         
        ///<response code="200">Assinatura de empresa atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaAssinaturaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EmpresaAssinaturaInputModel empresaAssinatura)
        {
            // Busca o registro existente
            var empresaAssinaturaExistente = await _empresaAssinaturaServico.BuscarPorID(empresaAssinatura.EmpresaAssinaturaId);
            if (empresaAssinaturaExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(empresaAssinatura, empresaAssinaturaExistente); // Faz o merge

            var retorno = await _empresaAssinaturaServico.Atualizar(_mapper.Map<EmpresaAssinatura>(empresaAssinaturaExistente));
            return Ok(_mapper.Map<EmpresaAssinaturaInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma assinatura de empresa.
        /// </summary>         
        ///<response code="200">Assinatura de empresa excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            await _empresaAssinaturaServico.Deletar(id);
            return Ok();
        }
    }
}
