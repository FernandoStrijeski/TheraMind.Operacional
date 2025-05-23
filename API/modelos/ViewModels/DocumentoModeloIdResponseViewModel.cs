using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class DocumentoModeloIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do modelo de documento criado
        /// </summary>
        public int DocumentoModeloId { get; set; }

        public DocumentoModeloIdResponseViewModel(int id)
        {
            DocumentoModeloId = id;
        }
    }
}
