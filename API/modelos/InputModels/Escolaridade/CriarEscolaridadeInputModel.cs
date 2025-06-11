using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEscolaridadeInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarEscolaridadeInputModelValidator : AbstractValidator<CriarEscolaridadeInputModel>
    {
        public CriarEscolaridadeInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição da escolaridade!");            
        }
    }
}
