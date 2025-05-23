using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarDocumentoModeloEmpresaInputModel
    {
        public int DocumentoModeloEmpresaID { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string Conteudo { get; set; } = null!;
        public bool? Ativo { get; set; }       
    }

    public class CriarDocumentoModeloEmpresaInputModelValidator : AbstractValidator<CriarDocumentoModeloEmpresaInputModel>
    {
        public CriarDocumentoModeloEmpresaInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe o título do documento!");
            RuleFor(x => x.Conteudo).NotEmpty().WithMessage("Por favor, informe o conteúdo do documento!");
        }
    }
}
