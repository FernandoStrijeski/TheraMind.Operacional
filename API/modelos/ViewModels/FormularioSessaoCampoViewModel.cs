namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class FormularioSessaoCampoViewModel
    {
        public int FormularioSessaoCampoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public string NomeCampo { get; set; } = null!;
        public bool? Ativo { get; set; }        
    }
}
