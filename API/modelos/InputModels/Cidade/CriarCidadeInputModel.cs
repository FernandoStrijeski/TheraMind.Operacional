using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarCidadeInputModel
    {
        public int PaisId { get; set; }
        public string Uf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public int CodigoIbge { get; set; }
    }

    public class CriarCidadeInputModelValidator : AbstractValidator<CriarCidadeInputModel>
    {
        public CriarCidadeInputModelValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150).WithMessage("Por favor, informe o nome da cidade!");            
            RuleFor(x => x.CodigoIbge).NotNull().WithMessage("Por favor, informe o código do IBGE!");            
        }
    }
}
