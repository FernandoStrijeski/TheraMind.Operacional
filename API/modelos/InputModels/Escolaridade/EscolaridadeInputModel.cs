using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EscolaridadeInputModel
    {
        /// <summary>
        /// Id da escolaridade
        /// </summary>
        public int EscolaridadeId { get; set; }

        /// <summary>
        /// Descrição da escolaridade
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class EscolaridadeValidator : AbstractValidator<EscolaridadeInputModel>
    {
        public EscolaridadeValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
