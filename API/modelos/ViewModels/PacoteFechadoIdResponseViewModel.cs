using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class PacoteFechadoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do pacote fechado criado
        /// </summary>
        public int PacoteFechadoId { get; set; }

        public PacoteFechadoIdResponseViewModel(int id)
        {
            PacoteFechadoId = id;
        }
    }
}
