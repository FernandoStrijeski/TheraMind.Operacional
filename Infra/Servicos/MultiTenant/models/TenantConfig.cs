using System.Collections.Generic;

namespace Infra.Servicos.MultiTenant.models
{
    public class TenantConfig
    {
        public DefaultConfig Default { get; set; }
        public List<ConnectionParams> Tenants { get; set; }
    }
}
