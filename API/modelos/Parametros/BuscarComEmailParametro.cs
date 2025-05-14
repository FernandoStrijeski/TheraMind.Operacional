using System.ComponentModel.DataAnnotations;

namespace API.modelos
{
    public class BuscarComEmailParametro
    {
        /// <summary>
        /// E-mail
        /// </summary>
        /// <example></example>
        [Required]
        public string Email { get; set; }
       
    }
}
