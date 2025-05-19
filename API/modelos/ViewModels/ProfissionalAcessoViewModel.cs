namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class ProfissionalAcessoViewModel
    {
        public int ProfissionalAcessoId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short AcessoTipo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
