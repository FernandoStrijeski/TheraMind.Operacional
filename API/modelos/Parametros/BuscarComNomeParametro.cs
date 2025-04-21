using System.ComponentModel.DataAnnotations;

namespace API.modelos
{
    public class BuscarComNomeParametro
    {
        /// <summary>
        /// Nome
        /// </summary>
        /// <example></example>
        [Required]
        public string Nome { get; set; }
       
    }
}
