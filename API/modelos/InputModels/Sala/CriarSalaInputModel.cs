using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarSalaInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
    }

    public class CriarSalaInputModelValidator : AbstractValidator<CriarSalaInputModel>
    {
        public CriarSalaInputModelValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe o nome da sala!");            
        }
    }
}
