namespace Dominio.Entidades
{
    public class TenantCliente
    {
        public string Tenant { get; private set; }
        public string IDExterno { get; private set; }
        public string UrlAPI { get; private set; }
        public int UrlAPITipo { get; set; }
    }
}