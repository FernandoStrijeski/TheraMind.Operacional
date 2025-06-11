using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarPaisInputModel
    {
        public string Nome { get; set; } = null!;        
    }

    public class CriarPaisInputModelValidator : AbstractValidator<CriarPaisInputModel>
    {
        public CriarPaisInputModelValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do país!");            
        }
    }
}
