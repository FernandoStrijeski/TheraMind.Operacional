using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEstadoCivilInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarEstadoCivilInputModelValidator : AbstractValidator<CriarEstadoCivilInputModel>
    {
        public CriarEstadoCivilInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do estado civil!");            
        }
    }
}
