using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ServicoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do serviço criado
        /// </summary>
        public int ServicoId { get; set; }

        public ServicoIdResponseViewModel(int id)
        {
            ServicoId = id;
        }
    }
}
