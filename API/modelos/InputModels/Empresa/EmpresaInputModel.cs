using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EmpresaInputModel
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

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class EmpresaValidator : AbstractValidator<EmpresaInputModel>
    {
        public EmpresaValidator()
        {
            RuleFor(x => x.RazaoSocial).MaximumLength(255);
            RuleFor(x => x.NomeFantasia).MaximumLength(255);                        
        }
    }
}
