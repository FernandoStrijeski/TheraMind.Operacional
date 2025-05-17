using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarProfissionalInputModel
    {
        public Guid ProfissionalId { get; set; }
        public string TipoProfissional { get; set; } = null!;
        public string TipoPessoa { get; set; } = null!;
        public string NomeCompleto { get; set; } = null!;
        public string? AreaAtuacao { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? Crp { get; set; }
        public string? Crfa { get; set; }
        public string? Crefito { get; set; }
        public string? Crm { get; set; }
        public string? Crn { get; set; }
        public string? Coffito { get; set; }
        public string Sexo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public Guid? UsuarioID { get; set; } = null!;
        public bool? Ativo { get; set; }        
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
