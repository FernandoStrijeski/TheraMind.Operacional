using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class SalaIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da sala criada
        /// </summary>
        public string SalaId { get; set; }

        public SalaIdResponseViewModel(string id)
        {
            SalaId = id;
        }
    }
}
