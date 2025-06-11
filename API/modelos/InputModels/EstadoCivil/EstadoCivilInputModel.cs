using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EstadoCivilInputModel
    {
        /// <summary>
        /// Id do estado civil
        /// </summary>
        public string EstadoCivilId { get; set; }

        /// <summary>
        /// Descrição do estado civil
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class EstadoCivilValidator : AbstractValidator<EstadoCivilInputModel>
    {
        public EstadoCivilValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
