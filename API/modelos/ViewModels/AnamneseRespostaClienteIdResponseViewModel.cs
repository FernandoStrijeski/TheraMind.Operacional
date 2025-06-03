using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseRespostaClienteIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da questão respondida do subgrupo da anamnese criada
        /// </summary>
        public int anamneseSubGrupoQuestaoID { get; set; }

        public AnamneseRespostaClienteIdResponseViewModel(int id)
        {
            anamneseSubGrupoQuestaoID = id;
        }
    }
}
