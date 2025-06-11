using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarGrauParentescoInputModel
    {
        public string Descricao { get; set; } = null!;        
    }

    public class CriarGrauParentescoInputModelValidator : AbstractValidator<CriarGrauParentescoInputModel>
    {
        public CriarGrauParentescoInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do grau de parentesco!");            
        }
    }
}
