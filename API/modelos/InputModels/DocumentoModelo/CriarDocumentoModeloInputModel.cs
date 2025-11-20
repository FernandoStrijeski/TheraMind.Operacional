using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarDocumentoModeloInputModel
    {
        public int TipoDocumentoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string ConteudoTexto { get; set; } = null!;
        public byte[] ConteudoArquivo { get; set; } = null!;
        public bool? Ativo { get; set; }       
    }

    public class CriarDocumentoModeloInputModelValidator : AbstractValidator<CriarDocumentoModeloInputModel>
    {
        public CriarDocumentoModeloInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe o título do documento!");
        }
    }
}
