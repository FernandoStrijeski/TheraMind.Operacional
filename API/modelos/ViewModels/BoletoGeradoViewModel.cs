using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class BoletoGeradoViewModel
    {
        public string NossoNumero { get; set; }
        public string PdfBase64 { get; set; }

        public BoletoGeradoViewModel(string nossoNumero, byte[] pdf)
        {
            NossoNumero = nossoNumero;
            PdfBase64 = Convert.ToBase64String(pdf);
        }
    }
}
