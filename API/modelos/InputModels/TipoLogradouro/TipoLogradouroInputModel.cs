using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class TipoLogradouroInputModel
    {
        /// <summary>
        /// Id do Tipo de Logradouro
        /// </summary>
        public string TipoLogradouroId { get; set; }

        /// <summary>
        /// Descrição do Tipo de Logradouro
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class TipoLogradouroValidator : AbstractValidator<TipoLogradouroInputModel>
    {
        public TipoLogradouroValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(255);            
        }
    }
}
