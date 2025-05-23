using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class DocumentoModeloEmpresaIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do modelo de documento criado
        /// </summary>
        public int DocumentoModeloEmpresaId { get; set; }

        public DocumentoModeloEmpresaIdResponseViewModel(int id)
        {
            DocumentoModeloEmpresaId = id;
        }
    }
}
