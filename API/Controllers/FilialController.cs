using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Filiais;
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
    public class FilialController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFilialServico _filialServico;
        private readonly IHttpContextAccessor _httpContext;

        public FilialController(
            IMapper mapper,
            IFilialServico filialServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _filialServico = filialServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca a filial a partir dos identificadores informados
        /// </summary>
        /// <response code="200">Retorna a filial pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(FilialViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR,CLIENTE")]
        public async Task<ActionResult> BuscarPorID(int filialID)
        {
            Filial? filial = await _filialServico.BuscarPorID(filialID);
            if (filial == null)
                return NotFound("Nenhuma filial encontrada"); ;

            var resultado = _mapper.Map<FilialViewModel>(filial);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca a filial pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<FilialViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var filial = await _filialServico.BuscarPorNome(parametro);

            if (filial == null || filial.Count == 0)
                return NotFound("Nenhuma filial encontrada");

            var resultado = _mapper.Map<List<FilialViewModel>>(filial);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca todas as filiais da empresa
        /// </summary>
        /// <param name="empresaID"></param>
        /// <returns></returns>
        [HttpGet("ObterTodasPorEmpresa")]
        [ProducesResponseType(
            typeof(List<FilialViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorEmpresa([FromQuery] Guid empresaID)
        {
            var filiais = await _filialServico.BuscarTodasPorEmpresa(empresaID);

            if (filiais == null || filiais.Count == 0)
                return NotFound("Nenhuma filial encontrada");

            var resultado = _mapper.Map<List<FilialViewModel>>(filiais);
            return Ok(resultado);
        }

        /// <summary>
        /// Cria uma filial.
        /// </summary>         
        ///<response code="201">Filial criada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FilialViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarFilialInputModel filial)
        {
            var retorno = await _filialServico.Adicionar(_mapper.Map<Filial>(filial));
            return Ok(_mapper.Map<FilialViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza uma filial.
        /// </summary>         
        ///<response code="200">Filial atualizada com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(FilialViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] FilialInputModel filial)
        {
            // Busca o registro existente
            var filialExistente = await _filialServico.BuscarPorID(filial.FilialId);
            if (filialExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(filial, filialExistente); // Faz o merge

            var retorno = await _filialServico.Atualizar(_mapper.Map<Filial>(filialExistente));
            return Ok(_mapper.Map<FilialInputModel>(retorno));
        }

        /// <summary>
        /// Exclui uma filial.
        /// </summary>         
        ///<response code="200">Filial excluída com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int filialID)
        {
            await _filialServico.Deletar(filialID);
            return Ok();
        }
    }
}
