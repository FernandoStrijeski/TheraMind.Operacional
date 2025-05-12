using Dominio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class CidadeViewModel
    {
        [Key]
        public int CidadeId { get; set; }
        public string Nome { get; set; } = null!;
        public int CodigoIbge { get; set; }        
    }
}
