using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace API.modelos.InputModels
{    
    public class CriarProfissionalInputModel
    {
        /// <summary>
        /// Tipo de profissional Dono da Clínica, Psicólogo, Terapeura, etc
        /// </summary>
        [Required]
        public string TipoProfissional { get; set; } = null!;

        /// <summary>
        /// Tipo de pessoa (PF, PJ)
        /// </summary>
        [Required]
        public string TipoPessoa { get; set; } = null!;

        /// <summary>
        /// Nome completo do profissional
        /// </summary>
        [Required]
        public string NomeCompleto { get; set; } = null!;

        /// <summary>
        /// Área de atuação Ex. (4,7) 0-Fisioterapia, 1-Fonoaudiologia, 2-Medicina, 3-Nutrição, 4-Psicologia, 5-Psicopedagogia, 6-Terapia Ocupacional, 7-Terapias Gerais	
        /// </summary>
        public string? AreaAtuacao { get; set; }

        /// <summary>
        /// CPF somente números em caso de Pessoa Física (PF)
        /// </summary>
        public string? Cpf { get; set; }

        /// <summary>
        /// CNPJ somente números em caso de Pessoa Jurídica (PJ)
        /// </summary>
        public string? Cnpj { get; set; }
        /// <summary>
        /// CRP Completo em caso de Psicólogo
        /// </summary>
        public string? Crp { get; set; }

        /// <summary>
        /// CRFA completo em caso de Fonoaudiológico
        /// </summary>
        public string? Crfa { get; set; }

        /// <summary>
        /// Crefito completo em caso de Fisioterapeuta
        /// </summary>
        public string? Crefito { get; set; }

        /// <summary>
        /// CRM completo em caso de médico
        /// </summary>
        public string? Crm { get; set; }

        /// <summary>
        /// CRN completo em caso de Nutricionista
        /// </summary>
        public string? Crn { get; set; }

        /// <summary>
        /// Coffito em caso de Terapeuta ocupacional
        /// </summary>
        public string? Coffito { get; set; }

        /// <summary>
        /// Sexo "masculino" ou "feminino"
        /// </summary>
        [Required]
        public string Sexo { get; set; } = null!;

        /// <summary>
        /// E-mail do profissional para login
        /// </summary>
        [Required]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Celular somente numeros contendo 55 + DDD + numero
        /// </summary>
        [Required]
        public string Celular { get; set; } = null!;

        /// <summary>
        /// Id do usuário utilizado no login
        /// </summary>
        public Guid? UsuarioID { get; set; } = null!;

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        public UsuarioInputModel UsuarioInputModel { get; set; } = new UsuarioInputModel();
    }

    public class CriarProfissionalInputModelValidator : AbstractValidator<CriarProfissionalInputModel>
    {
        public CriarProfissionalInputModelValidator()
        {
            RuleFor(x => x.NomeCompleto).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe o nome completo do profissional!");
            RuleFor(x => x.TipoProfissional).NotEmpty().MaximumLength(50).WithMessage("Por favor, informe o tipo de profissional!");
            RuleFor(x => x.TipoPessoa).NotEmpty().MaximumLength(2).WithMessage("Por favor, informe o tipo de pessoa!");
            RuleFor(x => x.AreaAtuacao).NotEmpty().MaximumLength(30).WithMessage("Por favor, informe a área de atuação!");
            RuleFor(x => x.Cpf).MaximumLength(11).WithMessage("O CPF deve ter até 11 caracteres somente números!");
            RuleFor(x => x.Cnpj).MaximumLength(14).WithMessage("O CNPJ deve ter até 14 caracteres somente números!");
            RuleFor(x => x.Crp).MaximumLength(20);
            RuleFor(x => x.Crfa).MaximumLength(20);
            RuleFor(x => x.Crefito).MaximumLength(20);
            RuleFor(x => x.Crm).MaximumLength(20);
            RuleFor(x => x.Crn).MaximumLength(20);
            RuleFor(x => x.Coffito).MaximumLength(20);
        }
    }
}
