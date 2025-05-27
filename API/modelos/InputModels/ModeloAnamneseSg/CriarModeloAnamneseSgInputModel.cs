using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarModeloAnamneseSgInputModel
    {
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public int ModeloAnamneseSgid { get; set; }        
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }        
    }

    public class CriarModeloAnamneseSgInputModelValidator : AbstractValidator<CriarModeloAnamneseSgInputModel>
    {
        public CriarModeloAnamneseSgInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o título do subgrupo!");            
        }
    }
}
