using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infra.Core.Servicos;
using Microsoft.Extensions.Logging;

namespace Infra.Servicos.TheraMindAutenticacao
{
    public class TheraMindAutenticacaoServico : HttpClientBase, ITheraMindAutenticacaoServico
    {
        public TheraMindAutenticacaoServico(
            HttpClient httpClient,
            ILogger<TheraMindAutenticacaoServico> logger
        ) : base(logger)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarToken(string token)
        {
            string url = Environment.GetEnvironmentVariable("URL_VALIDACAO_TOKEN");
            var statusCode = await GetSemResponse($"{url}/{token}");

            return (statusCode != HttpStatusCode.OK);
        }
    }
}
