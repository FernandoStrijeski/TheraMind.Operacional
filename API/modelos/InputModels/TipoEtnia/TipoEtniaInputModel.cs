using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class TipoEtniaInputModel
    {
        /// <summary>
        /// Id do Tipo de Etnia
        /// </summary>
        public int TipoEtniaId { get; set; }

        /// <summary>
        /// Descrição do Tipo de Etnia
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class TipoEtniaValidator : AbstractValidator<TipoEtniaInputModel>
    {
        public TipoEtniaValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
