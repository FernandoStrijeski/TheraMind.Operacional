namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class DocumentoModeloEmpresaViewModel
    {
        public int DocumentoModeloEmpresaID { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int TipoDocumentoId { get; set; }

        public virtual TipoDocumentoViewModel TipoDocumento { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string Conteudo { get; set; } = null!;
        public bool? Ativo { get; set; }
    }
}
