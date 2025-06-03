namespace API.Operacional.modelos.ViewModels
{
    public class ConvenioViewModel
    {
        public int ConvenioId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public short TipoRepasse { get; set; }
        public decimal ValorRepasse { get; set; }
        public bool? Ativo { get; set; }
    }
}
