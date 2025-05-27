namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class AcompanhamentoClinicoViewModel
    {
        public Guid AcompanhamentoClinicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid? ClienteId { get; set; }
        public string? AvaliacaoDemanda { get; set; }
        public string? PlanoTratamento { get; set; }
        public string? Diagnostico { get; set; }
        public string? RegistroEncerramento { get; set; }        
    }
}
