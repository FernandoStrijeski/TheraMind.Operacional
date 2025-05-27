using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarModeloAnamneseGInputModel
    {
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }        
    }

    public class CriarModeloAnamneseGInputModelValidator : AbstractValidator<CriarModeloAnamneseGInputModel>
    {
        public CriarModeloAnamneseGInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe o título do modelo!");            
        }
    }
}
