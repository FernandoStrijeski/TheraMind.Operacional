using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoQuestaoViewModel
    {
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcaoViewModel> AnamneseSubGrupoQuestaoOpcoes { get; set; }
    }
}
