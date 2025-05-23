using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AgendaSessaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da sessão da agenda criada
        /// </summary>
        public Guid AgendaSessaoId { get; set; }

        public AgendaSessaoIdResponseViewModel(Guid id)
        {
            AgendaSessaoId = id;
        }
    }
}
