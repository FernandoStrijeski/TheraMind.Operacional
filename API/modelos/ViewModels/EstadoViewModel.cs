using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API.Operacional.modelos.ViewModels
{
    public class EstadoViewModel
    {
        [Key]  // Define a chave primária
        public string Uf { get; set; } = null!;
        public string Descricao { get; set; } = null!;

        public ICollection<CidadeViewModel> Cidades { get; set; }
    }
}
