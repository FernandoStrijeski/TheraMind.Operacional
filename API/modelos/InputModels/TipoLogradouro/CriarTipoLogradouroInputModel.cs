using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarTipoLogradouroInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarTipoLogradouroInputModelValidator : AbstractValidator<CriarTipoLogradouroInputModel>
    {
        public CriarTipoLogradouroInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do tipo de logradouro!");            
        }
    }
}
