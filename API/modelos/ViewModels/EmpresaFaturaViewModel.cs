namespace API.Operacional.modelos.ViewModels
{
    public class EmpresaFaturaViewModel
    {
        public int EmpresaFaturaId { get; set; }
        public Guid EmpresaAssinaturaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid PlanoId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal Valor { get; set; }
        public short? FormaPagamento { get; set; }
        public byte[]? Anexo { get; set; }
        public short Situacao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public bool? Ativo { get; set; }        
    }
}
