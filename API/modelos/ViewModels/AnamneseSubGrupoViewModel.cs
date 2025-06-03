using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoViewModel
    {
        public int AnamneseSubGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoViewModel> AnamneseSubGrupoQuestoes { get; set; }

    }
}
