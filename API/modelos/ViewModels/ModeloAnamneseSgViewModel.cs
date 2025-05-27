namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class ModeloAnamneseSgViewModel
    {
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
    }
}
