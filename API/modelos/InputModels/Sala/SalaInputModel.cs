using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class SalaInputModel
    {
        /// <summary>
        /// ID da Sala
        /// </summary>
        [Required]
        public string SalaId { get; set; } = null!;

        /// <summary>
        /// ID da Empresa
        /// </summary>
        [Required]
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// ID da Filial
        /// </summary>
        [Required]
        public int FilialId { get; set; }

        /// <summary>
        /// Nome da Sala
        /// </summary>
        [Required]
        public string Nome { get; set; } = null!;
        
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class SalaValidator : AbstractValidator<SalaInputModel>
    {
        public SalaValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(255);
        }
    }
}
