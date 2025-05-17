using Dominio.Entidades;

namespace API.AdmissaoDigital.modelos.ViewModels
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
