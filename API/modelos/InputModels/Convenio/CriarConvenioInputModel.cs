using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarConvenioInputModel
    {
        public int ConvenioId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public short TipoRepasse { get; set; }
        public decimal ValorRepasse { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarConvenioInputModelValidator : AbstractValidator<CriarConvenioInputModel>
    {
        public CriarConvenioInputModelValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(150).WithMessage("Por favor, informe o nome completo do convênio!");            
        }
    }
}
