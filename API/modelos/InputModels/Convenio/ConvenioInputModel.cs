using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ConvenioInputModel
    {
        /// <summary>
        /// Id do convênio
        /// </summary>
        public int ConvenioId { get; set; }

        /// <summary>
        /// Id da Empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da Filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Nome do convênio
        /// </summary>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Tipo de repasse (0-Dinheiro, 1-Percentual)
        /// </summary>
        public short TipoRepasse { get; set; }

        /// <summary>
        /// Valor fixo ou valor do percentual do repasse
        /// </summary>
        public decimal ValorRepasse { get; set; }
        
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class ConvenioValidator : AbstractValidator<ConvenioInputModel>
    {
        public ConvenioValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(150);            
        }
    }
}
