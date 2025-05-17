namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class TipoDocumentoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do tipo de documento
        /// </summary>
        public int TipoDocumentoId { get; set; }

        public TipoDocumentoIdResponseViewModel(int id)
        {
            TipoDocumentoId = id;
        }
    }
}
