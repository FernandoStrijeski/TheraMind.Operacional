using System.ComponentModel.DataAnnotations;

namespace API.AdmissaoDigital.DTO.Empresa
{
    public class EmpresaDTO
    {        
        /// <summary>
        /// Identificador da empresa
        /// </summary>
        [Required]
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Razão Social
        /// </summary>
        [Required]
        public string RazaoSocial { get; set; }

        /// <summary>
        /// Nome Fantasia
        /// </summary>
        [Required]
        public string NomeFantasia { get; set; }

        /// <summary>
        /// Logotipo
        /// </summary>        
        public byte[]? Logotipo { get; set; }
    }
}