using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AgendaProfissionalIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da agenda profissional criada
        /// </summary>
        public int AgendaProfissionalId { get; set; }

        public AgendaProfissionalIdResponseViewModel(int id)
        {
            AgendaProfissionalId = id;
        }
    }
}
