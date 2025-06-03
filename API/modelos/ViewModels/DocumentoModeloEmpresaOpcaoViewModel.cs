namespace API.Operacional.modelos.ViewModels
{
    public class DocumentoModeloEmpresaOpcaoViewModel
    {
        public int DocumentoModeloEmpresaOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short TipoOpcao { get; set; }
        public string ConteudoBase64 { get; set; } = null!;
        public decimal? Transparencia { get; set; }
        public bool? Ativo { get; set; }
    }
}
