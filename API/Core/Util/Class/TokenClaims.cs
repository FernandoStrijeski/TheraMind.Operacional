namespace AdmissaoDigital.Core.Utils.Class
{
    public class TokenClaims
    {
        public string Cpf { get; set; }
        public string? Email { get; set; }
        public string NomeCompleto { get; set; }
        public string OrganizacaoId { get; set; }
        public string TipoUsuario { get; set; }
        public string? Celular { get; set; }
        public string Alias { get; set; }
        public List<string>? Empresas { get; set; } = new List<string>();
    }

}
