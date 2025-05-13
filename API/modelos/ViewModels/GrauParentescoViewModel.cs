using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class GrauParentescoViewModel
    {
        [Key]  // Define a chave primária
        public int GrauParentescoID { get; set; }
        public string Descricao { get; set; } = null!;
    }
}
