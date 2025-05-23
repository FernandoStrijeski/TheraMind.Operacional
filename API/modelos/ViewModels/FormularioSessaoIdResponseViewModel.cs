using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class FormularioSessaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do formulário de sessão criado
        /// </summary>
        public int FormularioSessaoId { get; set; }

        public FormularioSessaoIdResponseViewModel(int id)
        {
            FormularioSessaoId = id;
        }
    }
}
