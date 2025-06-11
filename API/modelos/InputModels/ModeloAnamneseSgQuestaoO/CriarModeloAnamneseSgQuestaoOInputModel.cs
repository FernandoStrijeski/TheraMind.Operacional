using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarModeloAnamneseSgQuestaoOInputModel
    {
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public string? Texto { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarModeloAnamneseSgQuestaoOInputModelValidator : AbstractValidator<CriarModeloAnamneseSgQuestaoOInputModel>
    {
        public CriarModeloAnamneseSgQuestaoOInputModelValidator()
        {
            RuleFor(x => x.Texto).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o texto da opção!");            
        }
    }
}
