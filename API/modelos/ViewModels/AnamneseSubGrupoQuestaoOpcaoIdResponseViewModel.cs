using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoQuestaoOpcaoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da opção da questão do subgrupo da anamnese criada
        /// </summary>
        public int AnamneseSubGrupoQuestaoOpcaoId { get; set; }

        public AnamneseSubGrupoQuestaoOpcaoIdResponseViewModel(int id)
        {
            AnamneseSubGrupoQuestaoOpcaoId = id;
        }
    }
}
