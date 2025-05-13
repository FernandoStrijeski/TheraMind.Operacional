namespace AdmissaoDigital.Core.Utils.Class
{
    public class TokenClaims
    {
        public string NameIdentifier { get; set; }
        public string? Email { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Alias { get; set; }
    }

    public class InformacoesAudit
    {
        public Guid UsuarioId { get; set; }
        public string PerfilAcesso { get; set; }
        public string IPAcesso { get; set; }
    }
}
