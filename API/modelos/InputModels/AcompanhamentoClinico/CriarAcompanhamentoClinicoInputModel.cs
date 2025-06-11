using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAcompanhamentoClinicoInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid? ClienteId { get; set; }
        public string? AvaliacaoDemanda { get; set; }
        public string? PlanoTratamento { get; set; }
        public string? Diagnostico { get; set; }
        public string? RegistroEncerramento { get; set; }
    }

    public class CriarAcompanhamentoClinicoInputModelValidator : AbstractValidator<CriarAcompanhamentoClinicoInputModel>
    {
        public CriarAcompanhamentoClinicoInputModelValidator()
        {
            
        }
    }
}
