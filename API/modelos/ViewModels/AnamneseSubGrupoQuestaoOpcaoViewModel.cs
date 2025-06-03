using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseSubGrupoQuestaoOpcaoViewModel
    {
        public int AnamneseSubGrupoQuestaoOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public string Texto { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
