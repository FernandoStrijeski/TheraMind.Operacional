namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseSgQuestaoOViewModel
    {
        public int ModeloAnamneseSgQuestaoOid { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public string? Texto { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
