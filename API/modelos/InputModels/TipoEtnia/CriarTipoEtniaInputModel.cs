using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarTipoEtniaInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarTipoEtniaInputModelValidator : AbstractValidator<CriarTipoEtniaInputModel>
    {
        public CriarTipoEtniaInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do tipo de etnia!");            
        }
    }
}
