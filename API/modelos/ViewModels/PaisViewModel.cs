using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class PaisViewModel
    {
        public int PaisID { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<EstadoViewModel> Estados { get; set; }
    }
}
