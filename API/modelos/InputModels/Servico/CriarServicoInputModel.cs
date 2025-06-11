using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarServicoInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool Padrao { get; set; }
        public short? DuracaoMinutos { get; set; }       
        public bool? Ativo { get; set; }
    }

    public class CriarServicoInputModelValidator : AbstractValidator<CriarServicoInputModel>
    {
        public CriarServicoInputModelValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe o nome do serviço!");            
        }
    }
}
