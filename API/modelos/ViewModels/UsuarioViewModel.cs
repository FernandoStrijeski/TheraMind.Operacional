namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid UsuarioId { get; set; }
        public Guid? EmpresaId { get; set; }
        public int? FilialId { get; set; }
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public bool TrocaSenhaProximoAcesso { get; set; }
        public string PerfilAcesso { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
