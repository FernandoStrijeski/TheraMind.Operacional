using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarIdentidadeGeneroInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarIdentidadeGeneroInputModelValidator : AbstractValidator<CriarIdentidadeGeneroInputModel>
    {
        public CriarIdentidadeGeneroInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição da identidade de gênero!");            
        }
    }
}
