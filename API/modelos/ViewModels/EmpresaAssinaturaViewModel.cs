namespace API.Operacional.modelos.ViewModels
{
    public class EmpresaAssinaturaViewModel
    {
        public Guid EmpresaAssinaturaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid PlanoId { get; set; }
        public short TipoPlano { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataExpiracao { get; set; }        
    }
}
