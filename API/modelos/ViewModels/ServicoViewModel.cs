namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class ServicoViewModel
    {
        public int ServicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool Padrao { get; set; }
        public short? DuracaoMinutos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
