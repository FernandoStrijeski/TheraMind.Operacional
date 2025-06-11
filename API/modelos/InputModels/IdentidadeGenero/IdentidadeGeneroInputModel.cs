using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class IdentidadeGeneroInputModel
    {
        /// <summary>
        /// Id da identidade de gênero
        /// </summary>
        public int IdentidadeGeneroId { get; set; }

        /// <summary>
        /// Descrição da identidade de gênero
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class IdentidadeGeneroValidator : AbstractValidator<IdentidadeGeneroInputModel>
    {
        public IdentidadeGeneroValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
