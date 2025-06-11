using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class NacionalidadeInputModel
    {
        /// <summary>
        /// Id da Nacionalidade
        /// </summary>
        public int NacionalidadeId { get; set; }

        /// <summary>
        /// Descrição da Nacionalidade
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class NacionalidadeValidator : AbstractValidator<NacionalidadeInputModel>
    {
        public NacionalidadeValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
