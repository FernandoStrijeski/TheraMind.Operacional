using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.DTO.Auditoria
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class AuditoriaDto
    {      
        /// <summary>
        /// Guid identificador único da auditoria.
        /// </summary>
        public Guid AuditoriaID { get; set; }

        /// <summary>
        /// Guid identificador único da Empresa.
        /// </summary>
        public Guid EmpresaID { get; set; }

        /// <summary>
        /// Guid identificador da Filial
        /// </summary>
        public int FilialID { get; set; }

        /// <summary>
        /// Tipo de ação
        /// </summary>
        [Required]
        public int TipoAcao { get; set; }

        /// <summary>
        /// Ação executada
        /// </summary>
        [Required]
        public string AcaoExecutada { get; set; }

        /// <summary>
        /// ID do Usuário
        /// </summary>
        [Required]
        public Guid UsuarioID { get; set; }

        /// <summary>
        /// ID do Cliente
        /// </summary>
        [Required]
        public string PerfilAcesso { get; set; }

        /// <summary>
        /// IP do Cliente
        /// </summary>
        [Required]
        public string IPAcesso { get; set; }

    }
}
