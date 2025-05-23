using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class FormularioSessaoInputModel
    {
        /// <summary>
        /// Id do formulário da sessão
        /// </summary>
        public int FormularioSessaoId { get; set; }

        //Id de empresa
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// ID da filial
        /// </summary>
        public int FilialId { get; set; }

        //Id dos serviços
        public int ServicoId { get; set; }

        /// <summary>
        /// Nome do formulário
        /// </summary>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Situação ativo ou não
        /// </summary>
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }

    public class FormularioSessaoValidator : AbstractValidator<FormularioSessaoInputModel>
    {
        public FormularioSessaoValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(150);            
        }
    }
}
