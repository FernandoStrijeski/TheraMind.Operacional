using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class TipoDocumentoInputModel
    {
        /// <summary>
        /// Identificador do tipo de documento
        /// </summary>
        [Required]
        public int TipoDocumentoId { get; set; }

        /// <summary>
        /// Descrição do tipo de documento
        /// </summary>
        [Required]
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class TipoDocumentoValidator : AbstractValidator<TipoDocumentoInputModel>
    {
        public TipoDocumentoValidator()
        {
            RuleFor(x => x.Descricao).MaximumLength(100);
        }
    }
}
