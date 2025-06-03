using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AnamneseRespostaClienteViewModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public Guid ClienteId { get; set; }
        public string Resposta { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
