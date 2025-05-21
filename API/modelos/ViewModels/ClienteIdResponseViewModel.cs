using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ClienteIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do convênio criado
        /// </summary>
        public Guid ClienteId { get; set; }

        public ClienteIdResponseViewModel(Guid id)
        {
            ClienteId = id;
        }
    }
}
