using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class DocumentoVariavelIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da variável do documento criada
        /// </summary>
        public int DocumentoVariavelId { get; set; }

        public DocumentoVariavelIdResponseViewModel(int id)
        {
            DocumentoVariavelId = id;
        }
    }
}
