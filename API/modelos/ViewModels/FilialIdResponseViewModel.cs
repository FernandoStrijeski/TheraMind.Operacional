using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class FilialIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da filial criada
        /// </summary>
        public int FilialId { get; set; }

        public FilialIdResponseViewModel(int id)
        {
            FilialId = id;
        }
    }
}
