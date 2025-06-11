using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class PaisInputModel
    {
        /// <summary>
        /// Id do Pais
        /// </summary>
        public int PaisId { get; set; }

        /// <summary>
        /// Nome do País
        /// </summary>
        public string Nome { get; set; } = null!;
    }

    public class PaisValidator : AbstractValidator<PaisInputModel>
    {
        public PaisValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(255);            
        }
    }
}
