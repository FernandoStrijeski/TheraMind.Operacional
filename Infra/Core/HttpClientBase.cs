using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infra.Core.Servicos
{
    public abstract class HttpClientBase
    {
        protected HttpClient _httpClient;
        private readonly ILogger<HttpClientBase> _logger;
        private HttpResponseMessage _ultimoResponse = null;

        public HttpClientBase()
        {
            _logger = null;
        }

        public HttpClientBase(ILogger<HttpClientBase> logger)
        {
            _logger = logger;
        }

        protected HttpResponseMessage GetUltimoResponse()
        {
            return _ultimoResponse;
        }

        protected async Task<HttpStatusCode> GetSemResponse(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            return response.StatusCode;
        }

        protected async Task<T> Get<T>(string url)
        {
            var (_, retorno) = await GetComResponse<T>(url);

            return retorno;
        }

        protected async Task<(string, T)> GetComResponse<T>(string url)
        {
            _ultimoResponse = null;
            var resposta = default(string);

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                _ultimoResponse = response;

                resposta = await RecuperaResposta(response);

                if (resposta != default(string))
                    return (resposta, JsonConvert.DeserializeObject<T>(resposta));
                else
                    return (resposta, default(T));
            }
            catch (Exception ex)
            {
                GravarLog(ex, $"Ocorreu um erro ao executar o método GET. Url: {url}");
                return (resposta, default(T));
            }
        }

        protected async Task<T> Post<T>(string url, HttpContent conteudo)
        {
            _ultimoResponse = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = conteudo
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                _ultimoResponse = response;

                string resposta = await RecuperaResposta(response);

                if (resposta != default(string))
                    return JsonConvert.DeserializeObject<T>(resposta);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                GravarLog(ex, $"Ocorreu um erro ao executar o método POST. Url: {url}");
                return default(T);
            }
        }

        protected async Task<T> Put<T>(string url, HttpContent conteudo)
        {
            _ultimoResponse = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url)
                {
                    Content = conteudo
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                _ultimoResponse = response;

                string resposta = await RecuperaResposta(response);

                if (resposta != default(string))
                    return JsonConvert.DeserializeObject<T>(resposta);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                GravarLog(ex, $"Ocorreu um erro ao executar o método PUT. Url: {url}");
                return default(T);
            }
        }

        protected async Task<T?> Patch<T>(string url, HttpContent conteudo)
        {
            _ultimoResponse = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url)
                {
                    Content = conteudo
                };

                HttpResponseMessage response = await _httpClient!.SendAsync(request);

                _ultimoResponse = response;

                string? resposta = await RecuperaResposta(response);

                if (resposta != default(string))
                    return JsonConvert.DeserializeObject<T>(resposta);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro ao executar o método Patch. Url: {url}");
                return default(T);
            }
        }

        private async Task<string> RecuperaResposta(HttpResponseMessage response)
        {
            var statusCodesErro = new HashSet<HttpStatusCode>()
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.BadGateway,
                HttpStatusCode.InternalServerError
            };

            if (response.StatusCode == HttpStatusCode.NoContent)
                return default(string);

            if (!statusCodesErro.Contains(response.StatusCode))
                return await response.Content.ReadAsStringAsync();
            else
                return default(string);
        }

        protected async Task<T> Delete<T>(string url, HttpContent conteudo)
        {
            _ultimoResponse = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = conteudo
                };

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                _ultimoResponse = response;

                string resposta = await RecuperaResposta(response);

                if (resposta != default(string))
                    return JsonConvert.DeserializeObject<T>(resposta);
                else
                    return default(T);
            }
            catch (Exception ex)
            {
                GravarLog(ex, $"Ocorreu um erro ao executar o método DELETE. Url: {url}");
                return default(T);
            }
        }

        protected void SetaCabecalho(string tipo, string valor) =>
            _httpClient.DefaultRequestHeaders.Add(tipo, valor);

        protected void SetaCabecalhoAuthorization(string tipo, string valor) =>
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                tipo,
                valor
            );

        protected void ResetaCabecalhoAuthorization() =>
            _httpClient.DefaultRequestHeaders.Authorization = null;

        protected void ResetaCabecalho() => _httpClient.DefaultRequestHeaders.Clear();

        private void GravarLog(Exception ex, string mensagem)
        {
            if (_logger == null)
            {
                return;
            }

            _logger.LogError(ex, mensagem);
        }
    }
}
