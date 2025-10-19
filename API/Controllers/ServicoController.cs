using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using API.Servicos.Servicos;
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
    public class ServicoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicoServico _servicoServico;
        private readonly IHttpContextAccessor _httpContext;

        public ServicoController(
            IMapper mapper,
            IServicoServico servicoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _servicoServico = servicoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Busca um serviço a partir do identificador informado
        /// </summary>
        /// <response code="200">Retorna o serviço pelo ID informado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpGet("ObterPorId")]
        [ProducesResponseType(
            typeof(ServicoViewModel),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorID(int servicoID)
        {
            Servico? servico = await _servicoServico.BuscarPorID(servicoID);
            if (servico == null)
                return NotFound("Nenhum serviço encontrado");


            var resultado = _mapper.Map<ServicoViewModel>(servico);
            return Ok(resultado);
        }

        /// <summary>
        /// Busca os serviços pelo nome
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        [HttpGet("ObterPorNome")]
        [ProducesResponseType(
            typeof(List<ServicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarPorNome([FromQuery] BuscarComNomeParametro parametro)
        {
            var servicos = await _servicoServico.BuscarPorNome(parametro);

            if (servicos == null || servicos.Count == 0)
                return NotFound("Nenhum serviço encontrado");

            var resultado = _mapper.Map<List<ServicoViewModel>>(servicos);
            return Ok(resultado);
        }


        /// <summary>
        /// Busca todos os serviços
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(
            typeof(List<ServicoViewModel>),
            StatusCodes.Status200OK
        )]
        [Authorize(Roles = "ADMIN,GESTOR")]
        public async Task<ActionResult> BuscarTodos()
        {
            var servicos = await _servicoServico.BuscarTodos();

            if (servicos == null || servicos.Count == 0)
                return NotFound("Nenhum serviço encontrado");


            var resultado = _mapper.Map<List<ServicoViewModel>>(servicos);
            return Ok(resultado);
        }


        /// <summary>
        /// Cria um serviço
        /// </summary>         
        ///<response code="201">Serviço criado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPost("Criar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ServicoViewModel), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] CriarServicoInputModel servico)
        {
            var retorno = await _servicoServico.Adicionar(_mapper.Map<Servico>(servico));
            return Ok(_mapper.Map<ServicoViewModel>(retorno));
        }

        /// <summary>
        /// Atualiza um serviço
        /// </summary>         
        ///<response code="200">Serviço atualizado com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpPut("Atualizar")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(ServicoViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Put([FromBody] ServicoInputModel servico)
        {
            // Busca o registro existente
            var servicoExistente = await _servicoServico.BuscarPorID(servico.ServicoId);
            if (servicoExistente == null)
                return NotFound();

            // Atualiza apenas os campos do InputModel, preservando o restante
            _mapper.Map(servico, servicoExistente); // Faz o merge

            var retorno = await _servicoServico.Atualizar(_mapper.Map<Servico>(servicoExistente));
            return Ok(_mapper.Map<ServicoViewModel>(retorno));
        }

        /// <summary>
        /// Exclui um serviço
        /// </summary>         
        ///<response code="200">Serviço excluído com sucesso.</response>
        ///<response code="401">Usuário não autorizado.</response>
        [HttpDelete("Excluir")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] int servicoID)
        {
            await _servicoServico.Deletar(servicoID);
            return Ok();
        }
    }
}
