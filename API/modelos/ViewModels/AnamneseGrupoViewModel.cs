using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseGrupoViewModel
    {
        public int AnamneseGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }

        public virtual ICollection<AnamneseSubGrupoViewModel> AnamneseSubGrupos { get; set; }
    }
}
