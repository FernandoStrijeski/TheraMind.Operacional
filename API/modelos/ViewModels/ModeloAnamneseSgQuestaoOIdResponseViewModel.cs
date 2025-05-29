using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseSgQuestaoOIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da opção da questão do subgrupo do grupo modelo de anamnese criado
        /// </summary>
        public int ModeloAnamneseSgQuestaoOId { get; set; }

        public ModeloAnamneseSgQuestaoOIdResponseViewModel(int id)
        {
            ModeloAnamneseSgQuestaoOId = id;
        }
    }
}
