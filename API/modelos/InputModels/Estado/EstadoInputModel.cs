using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class EstadoInputModel
    {
        /// <summary>
        /// Id do Estado
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        /// Id do País
        /// </summary>
        public int PaisID { get; set; }

        /// <summary>
        /// Descrição do Estado
        /// </summary>
        public string Descricao { get; set; } = null!;
    }

    public class EstadoValidator : AbstractValidator<EstadoInputModel>
    {
        public EstadoValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(100);            
        }
    }
}
