namespace API.Operacional.modelos.ViewModels
{
    public class PlanoViewModel
    {
        public Guid PlanoId { get; set; }
        public string NomePlano { get; set; } = null!;
        public decimal ValorPlanoMensal { get; set; }
        public decimal ValorPlanoAnual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
