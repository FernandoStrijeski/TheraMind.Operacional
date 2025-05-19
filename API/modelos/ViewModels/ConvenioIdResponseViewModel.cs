using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ConvenioIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do convênio criado
        /// </summary>
        public int ConvenioId { get; set; }

        public ConvenioIdResponseViewModel(int id)
        {
            ConvenioId = id;
        }
    }
}
