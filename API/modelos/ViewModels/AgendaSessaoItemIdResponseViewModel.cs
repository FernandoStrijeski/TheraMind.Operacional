using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AgendaSessaoItemIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do item da sessão da agenda criada
        /// </summary>
        public int AgendaSessaoItemId { get; set; }

        public AgendaSessaoItemIdResponseViewModel(int id)
        {
            AgendaSessaoItemId = id;
        }
    }
}
