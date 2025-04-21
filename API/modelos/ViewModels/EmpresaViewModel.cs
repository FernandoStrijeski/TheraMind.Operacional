namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class EmpresaViewModel
    {
        public Guid EmpresaId { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public byte[] Logotipo { get; set; }
        public bool Ativo {  get; set; }
        public DateTime DataCriacao { get; set; }            
    }
}
