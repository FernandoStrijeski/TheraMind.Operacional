using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarOrientacaoSexualInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarOrientacaoSexualInputModelValidator : AbstractValidator<CriarOrientacaoSexualInputModel>
    {
        public CriarOrientacaoSexualInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição da orientação sexual!");            
        }
    }
}
