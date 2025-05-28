using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseSgQuestaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da questão do subgrupo do grupo modelo de anamnese criado
        /// </summary>
        public int ModeloAnamneseSgQuestaoId { get; set; }

        public ModeloAnamneseSgQuestaoIdResponseViewModel(int id)
        {
            ModeloAnamneseSgQuestaoId = id;
        }
    }
}
