using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoQuestaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da questão do subgrupo da anamnese criada
        /// </summary>
        public int AnamneseSubGrupoQuestaoId { get; set; }

        public AnamneseSubGrupoQuestaoIdResponseViewModel(int id)
        {
            AnamneseSubGrupoQuestaoId = id;
        }
    }
}
