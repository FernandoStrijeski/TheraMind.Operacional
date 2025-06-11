using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class PlanoInputModel
    {
        /// <summary>
        /// Id do plano
        /// </summary>
        [Required]
        public Guid PlanoId { get; set; }

        /// <summary>
        /// Nome do plano
        /// </summary>
        [Required]
        public string NomePlano { get; set; } = null!;

        /// <summary>
        /// Valor da contratação mensal
        /// </summary>
        [Required]
        public decimal ValorPlanoMensal { get; set; }

        /// <summary>
        /// Valor da contratação anual
        /// </summary>
        [Required]
        public decimal ValorPlanoAnual { get; set; }

        /// <summary>
        /// Valor desconto promocional
        /// </summary>
        public decimal? DescontoPromocional { get; set; }

        /// <summary>
        /// Quantidade de meses para o desconto promocional
        /// </summary>
        public short? DescontoMeses { get; set; }
    
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class PlanoValidator : AbstractValidator<PlanoInputModel>
    {
        public PlanoValidator()
        {
            RuleFor(x => x.NomePlano).MaximumLength(50);
        }
    }
}
