using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarTipoDocumentoInputModel
    {
        public string Descricao { get; set; }        
        public bool Ativo { get; set; }
    }

    public class CriarTipoDocumentoInputModelValidator : AbstractValidator<CriarTipoDocumentoInputModel>
    {
        public CriarTipoDocumentoInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe o tipo de documento!");
        }
    }
}
