namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class DocumentoVariavelViewModel
    {
        public int DocumentoVariavelId { get; set; }
        public string NomeVariavel { get; set; } = null!;
        public string NomeCampo { get; set; } = null!;
        public string NomeTabela { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
