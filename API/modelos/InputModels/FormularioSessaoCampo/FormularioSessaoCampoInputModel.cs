using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class FormularioSessaoCampoInputModel
    {
        /// <summary>
        /// Id do campo do formulário da sessão
        /// </summary>
        public int FormularioSessaoCampoId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Id do serviço
        /// </summary>
        public int ServicoId { get; set; }

        /// <summary>
        /// Id do formulario de sessão
        /// </summary>
        public int FormularioSessaoId { get; set; }

        /// <summary>
        /// Nome do campo
        /// </summary>
        public string NomeCampo { get; set; } = null!;

        /// <summary>
        /// Situação do campo
        /// </summary>
        public bool? Ativo { get; set; }

    }

    public class FormularioSessaoCampoValidator : AbstractValidator<FormularioSessaoCampoInputModel>
    {
        public FormularioSessaoCampoValidator()
        {
            RuleFor(x => x.NomeCampo).MaximumLength(255);            
        }
    }
}
