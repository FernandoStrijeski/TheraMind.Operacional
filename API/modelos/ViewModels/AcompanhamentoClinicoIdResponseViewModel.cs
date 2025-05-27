using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AcompanhamentoClinicoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do acompanhamento clínico criado
        /// </summary>
        public Guid AcompanhamentoClinicoId { get; set; }

        public AcompanhamentoClinicoIdResponseViewModel(Guid id)
        {
            AcompanhamentoClinicoId = id;
        }
    }
}
