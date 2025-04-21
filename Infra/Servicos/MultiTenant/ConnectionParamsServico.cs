using System;
using System.Linq;
using Infra.Servicos.MultiTenant.models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infra.Servicos.MultiTenant
{
    public class ConnectionParamsServico : IConnectionParamsServico
    {
        protected readonly string _connectionString;
        private HttpContext _httpContext;
        protected readonly TenantConfig _tenantConfig;
        private ConnectionParams _connectionParamsAtual;

        public ConnectionParamsServico(
            IOptions<TenantConfig> tenantConfig,
            IHttpContextAccessor contextAccessor
        )
        {
            _tenantConfig = tenantConfig.Value;
            _httpContext = contextAccessor.HttpContext;
            if (_httpContext != null)
            {
                if (_httpContext.Request.Headers.TryGetValue("Alias", out var Alias))
                {
                    DefinirConnectionParamsAtual(Alias[0]);
                }
                else
                    throw new Exception("Não foi possível obter o Alias da conexão");
            }
        }

        public string PegarConnectionString()
        {
            return _connectionParamsAtual?.ConnectionString;
        }

        public ConnectionParams ObterConnectionsParams()
        {
            return _connectionParamsAtual;
        }

        private void DefinirConnectionParamsAtual(string Alias)
        {
            _connectionParamsAtual = _tenantConfig.Tenants.FirstOrDefault(t => t.Alias == Alias);
            if (_connectionParamsAtual == null)
                throw new Exception(
                    "Não foram encontradas configurações de conexão para o Alias Fornecido"
                );
        }
    }
}
