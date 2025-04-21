using Infra.Servicos.MultiTenant.models;

namespace Infra.Servicos.MultiTenant
{
    public interface IConnectionParamsServico
    {
        public string PegarConnectionString();
        public ConnectionParams ObterConnectionsParams();
    }
}
