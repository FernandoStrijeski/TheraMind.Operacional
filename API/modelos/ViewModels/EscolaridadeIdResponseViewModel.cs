using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class EscolaridadeIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da escolaridade criada
        /// </summary>
        public int EscolaridadeId { get; set; }

        public EscolaridadeIdResponseViewModel(int id)
        {
            EscolaridadeId = id;
        }
    }
}
