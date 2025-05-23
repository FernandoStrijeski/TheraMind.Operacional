using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class DocumentoModeloEmpresaOpcaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da opção para o modelo de documento criado
        /// </summary>
        public int DocumentoModeloEmpresaOpcaoId { get; set; }

        public DocumentoModeloEmpresaOpcaoIdResponseViewModel(int id)
        {
            DocumentoModeloEmpresaOpcaoId = id;
        }
    }
}
