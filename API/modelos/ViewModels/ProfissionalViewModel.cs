namespace API.Operacional.modelos.ViewModels
{
    public class ProfissionalViewModel
    {
        public Guid ProfissionalId { get; set; }
        public string TipoProfissional { get; set; } = null!;
        public string TipoPessoa { get; set; } = null!;
        public string NomeCompleto { get; set; } = null!;
        public string? AreaAtuacao { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? Crp { get; set; }
        public string? Crfa { get; set; }
        public string? Crefito { get; set; }
        public string? Crm { get; set; }
        public string? Crn { get; set; }
        public string? Coffito { get; set; }
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public Guid? UsuarioID { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
