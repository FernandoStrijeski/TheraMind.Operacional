namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class FormularioSessaoViewModel
    {
        public int FormularioSessaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
    }
}
