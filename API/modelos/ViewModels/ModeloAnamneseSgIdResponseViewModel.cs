using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseSgIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do subgrupo do grupo do modelo de anamnese criado
        /// </summary>
        public int ModeloAnamneseSgId { get; set; }

        public ModeloAnamneseSgIdResponseViewModel(int id)
        {
            ModeloAnamneseSgId = id;
        }
    }
}
