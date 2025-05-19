namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class PacoteFechadoViewModel
    {
        public int PacoteFechadoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int QuantidadeSessoes { get; set; }
        public decimal ValorTotal { get; set; }
        public bool? Ativo { get; set; }        
    }
}
