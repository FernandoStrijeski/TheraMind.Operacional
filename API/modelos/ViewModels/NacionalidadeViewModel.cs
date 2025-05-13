using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class NacionalidadeViewModel
    {
        [Key]  // Define a chave primária
        public int NacionalidadeID { get; set; }
        public string Descricao { get; set; } = null!;
    }
}
