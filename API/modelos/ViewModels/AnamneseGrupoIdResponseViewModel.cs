using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseGrupoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da anamnese criada
        /// </summary>
        public int AnamneseGrupoId { get; set; }

        public AnamneseGrupoIdResponseViewModel(int id)
        {
            AnamneseGrupoId = id;
        }
    }
}
