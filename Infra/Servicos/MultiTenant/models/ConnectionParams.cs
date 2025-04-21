namespace Infra.Servicos.MultiTenant.models
{
    public class ConnectionParams
    {
        public string Alias { get; set; }
        public string ServerName { get; set; }
        public string ServerType { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionString { get; set; }
        public string ReportConnectionString { get; set; }
        public string AuthorizationType { get; set; }
        public string XpoProvider { get; set; }
        public string ServerVersion { get; set; }
    }
}
