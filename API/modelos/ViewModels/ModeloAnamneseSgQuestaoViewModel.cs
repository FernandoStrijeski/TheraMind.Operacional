namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class ModeloAnamneseSgQuestaoViewModel
    {
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
    }
}
