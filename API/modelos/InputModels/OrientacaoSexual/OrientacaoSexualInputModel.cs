using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class OrientacaoSexualInputModel
    {
        /// <summary>
        /// Id da OrientacaoSexual
        /// </summary>
        public int OrientacaoSexualId { get; set; }

        /// <summary>
        /// Descrição da OrientacaoSexual
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class OrientacaoSexualValidator : AbstractValidator<OrientacaoSexualInputModel>
    {
        public OrientacaoSexualValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
