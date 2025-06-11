using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class GrauParentescoInputModel
    {
        /// <summary>
        /// Id do grau de parentesco
        /// </summary>
        public int GrauParentescoId { get; set; }

        /// <summary>
        /// Descrição do grau de parentesco
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class GrauParentescoValidator : AbstractValidator<GrauParentescoInputModel>
    {
        public GrauParentescoValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
