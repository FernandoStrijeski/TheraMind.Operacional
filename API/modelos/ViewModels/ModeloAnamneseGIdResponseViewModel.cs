using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseGIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do modelo de anamnese criado
        /// </summary>
        public int ModeloAnamneseGId { get; set; }

        public ModeloAnamneseGIdResponseViewModel(int id)
        {
            ModeloAnamneseGId = id;
        }
    }
}
