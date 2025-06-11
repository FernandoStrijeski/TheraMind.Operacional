using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarNacionalidadeInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarNacionalidadeInputModelValidator : AbstractValidator<CriarNacionalidadeInputModel>
    {
        public CriarNacionalidadeInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição da nacionalidade!");            
        }
    }
}
