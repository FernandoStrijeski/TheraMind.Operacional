using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarModeloAnamneseSgQuestaoInputModel
    {
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }

    public class CriarModeloAnamneseSgQuestaoInputModelValidator : AbstractValidator<CriarModeloAnamneseSgQuestaoInputModel>
    {
        public CriarModeloAnamneseSgQuestaoInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o título da questão!");            
        }
    }
}
