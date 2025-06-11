using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarDocumentoVariavelInputModel
    {
        public string NomeVariavel { get; set; } = null!;
        public string NomeCampo { get; set; } = null!;
        public string NomeTabela { get; set; } = null!;
        public bool? Ativo { get; set; }
    }

    public class CriarDocumentoVariavelInputModelValidator : AbstractValidator<CriarDocumentoVariavelInputModel>
    {
        public CriarDocumentoVariavelInputModelValidator()
        {
            RuleFor(x => x.NomeVariavel).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o nome da variável!");            
        }
    }
}
