namespace API.Operacional.modelos.ViewModels
{
    public class SalaViewModel
    {
        public string SalaId { get; set; } = null!;
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
