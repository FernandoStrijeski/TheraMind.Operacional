using API.Core.Filtros;
using API.Servicos.GeradorCNAB240Sicredi;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion(1.0)]
    [RequerValidacaoDeToken]
    public class RemessaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGeradorCNAB240SicrediServico _geradorCNAB240SicrediServico;
        private readonly IHttpContextAccessor _httpContext;

        public RemessaController(
            IMapper mapper,
            IGeradorCNAB240SicrediServico geradorCNAB240SicrediServico,
            IHttpContextAccessor httpContext
        )
        {
            _mapper = mapper;
            _geradorCNAB240SicrediServico = geradorCNAB240SicrediServico;
            _httpContext = httpContext;
        }

        [HttpPost("GerarRemessaCnab240")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GerarRemessaCnab240()
        {
            var bytesRemessa = _geradorCNAB240SicrediServico.GerarRemessaCnab240();
            string nome = $"SIC_{DateTime.Now:yyyyMMddHHmmss}.REM";
            string mensagemRetorno = _geradorCNAB240SicrediServico.EnviarRemessaParaSicredi(bytesRemessa, nome, out var retorno);
            if (String.IsNullOrEmpty(mensagemRetorno))
                return File(retorno ?? new byte[0], "text/plain", $"RET_{nome}");

            return StatusCode(500, "Erro ao enviar remessa para o Sicredi.");
        }

    }
}
