using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarPlanoInputModel
    {
        public string NomePlano { get; set; } = null!;
        public decimal ValorPlanoMensal { get; set; }
        public decimal ValorPlanoAnual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarPlanoInputModelValidator : AbstractValidator<CriarPlanoInputModel>
    {
        public CriarPlanoInputModelValidator()
        {
            RuleFor(x => x.NomePlano).NotEmpty().MaximumLength(50).WithMessage("Por favor, informe o nome do plano até 50 caracteres!");            
        }
    }
}
