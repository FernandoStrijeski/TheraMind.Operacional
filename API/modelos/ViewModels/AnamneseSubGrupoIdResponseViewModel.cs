using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do subgrupo da anamnese criada
        /// </summary>
        public int AnamneseSubGrupoId { get; set; }

        public AnamneseSubGrupoIdResponseViewModel(int id)
        {
            AnamneseSubGrupoId = id;
        }
    }
}
