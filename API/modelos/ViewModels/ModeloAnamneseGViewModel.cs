using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class ModeloAnamneseGViewModel
    {
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
        public virtual ICollection<ModeloAnamneseSgViewModel> ModeloAnamneseSubGrupos { get; set; }
    }
}
