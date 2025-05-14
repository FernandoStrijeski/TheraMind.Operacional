using API.AdmissaoDigital.modelos.ViewModels;
using API.Core.Filtros;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.Boletos;
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
    public class BoletoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBoletoServico _boletoServico;
        private readonly IHttpContextAccessor _httpContext;

        public BoletoController(
            IMapper mapper,
            IBoletoServico boletoServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _boletoServico = boletoServico;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Gerar o boleto
        /// </summary>
        /// <response code="200">Retorna o boleto gerado</response>
        /// <response code="401">Um token Bearer válido é necessário para autenticar a chamada</response>
        /// <response code="403">Token não é válido para esta requisição ou não possui credenciais necessárias</response>
        [HttpPost("GerarBoleto")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GerarBoleto([FromBody] CriarBoletoInputModel criarBoletoInputModel)
        {
            var pdfBytes = _boletoServico.GerarBoleto(criarBoletoInputModel);
            return File(pdfBytes, "application/pdf", "boleto.pdf");
        }
    }
}
