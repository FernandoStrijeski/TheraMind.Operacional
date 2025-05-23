using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarDocumentoModeloEmpresaOpcaoInputModel
    {
        public int DocumentoModeloEmpresaOpcaoID { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short TipoOpcao { get; set; }        
        public string ConteudoBase64 { get; set; } = null!;
        public decimal? Transparencia { get; set; }       
        public bool? Ativo { get; set; }       
    }

    public class CriarDocumentoModeloEmpresaOpcaoInputModelValidator : AbstractValidator<CriarDocumentoModeloEmpresaOpcaoInputModel>
    {
        public CriarDocumentoModeloEmpresaOpcaoInputModelValidator()
        {
            RuleFor(x => x.ConteudoBase64).NotEmpty().WithMessage("Por favor, informe o conteúdo do documento!");
        }
    }
}
