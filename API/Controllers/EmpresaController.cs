using API.Core.Filtros;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Empresas;
using Asp.Versioning;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(1.0)]
    [RequerValidacaoDeToken]
    public class EmpresaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmpresaServico _empresaServico;
        private readonly IHttpContextAccessor _httpContext;

        public EmpresaController(
            IMapper mapper,
            IEmpresaServico empresaServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _empresaServico = empresaServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a empresa a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna a empresa pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(Guid empresaID)
        {
            Empresa? empresa = await _empresaServico.BuscarPorID(empresaID);
            if (empresa == null)
                return NotFound("Nenhum tipo de etina encontrado");


            var resultado = _mapper.Map<EmpresaViewModel>(empresa);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todas as empresas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<EmpresaViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var empresa = await _empresaServico.BuscarTodos();

            if (empresa == null || empresa.Count == 0)
                return NotFound("Nenhuma empresa encontrada");


            var resultado = _mapper.Map<List<EmpresaViewModel>>(empresa);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria uma empresa.
        /// </summary>         
        ///<response code="201">Empresa criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarEmpresaInputModel empresa)
        {
            var retorno = await _empresaServico.Adicionar(_mapper.Map<Empresa>(empresa));
            return Ok(_mapper.Map<EmpresaViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma empresa.
        /// </summary>         
        ///<response code="200">Empresa atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(EmpresaViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] EmpresaInputModel empresa)
        {
            // Busca o registro existente
            var empresaExistente = await _empresaServico.BuscarPorID(empresa.EmpresaId);
            if (empresaExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(empresa, empresaExistente); // Faz o merge

            var retorno = await _empresaServico.Atualizar(_mapper.Map<Empresa>(empresaExistente));
            return Ok(_mapper.Map<EmpresaInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma empresa.
        /// </summary>         
        ///<response code="200">Empresa excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid empresaID)
        {
            await _empresaServico.Deletar(empresaID);
            return Ok();
        }
    }
}
