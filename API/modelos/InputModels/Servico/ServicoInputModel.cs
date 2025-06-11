using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ServicoInputModel
    {
        /// <summary>
        /// ID do serviço
        /// </summary>
        [Required]
        public int ServicoId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        [Required]
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da Filial
        /// </summary>
        [Required]
        public int FilialId { get; set; }

        /// <summary>
        /// Nome do serviço
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Serviço Padrão
        /// </summary>
        [Required]
        public bool Padrao { get; set; }

        /// <summary>
        /// Duração do serviço em minutos
        /// </summary>
        public short? DuracaoMinutos { get; set; }
        
        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class ServicoValidator : AbstractValidator<ServicoInputModel>
    {
        public ServicoValidator()
        {
            RuleFor(x => x.Nome).MaximumLength(255);
        }
    }
}
