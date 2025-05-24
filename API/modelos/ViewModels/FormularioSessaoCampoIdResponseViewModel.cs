using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class FormularioSessaoCampoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do campo do formulário de sessão criado
        /// </summary>
        public int FormularioSessaoCampoId { get; set; }

        public FormularioSessaoCampoIdResponseViewModel(int id)
        {
            FormularioSessaoCampoId = id;
        }
    }
}
