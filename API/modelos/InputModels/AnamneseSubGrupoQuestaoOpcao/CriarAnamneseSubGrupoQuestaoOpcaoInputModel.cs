using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAnamneseSubGrupoQuestaoOpcaoInputModel
    {
        public int AnamneseSubGrupoQuestaoOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public string Texto { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarAnamneseSubGrupoQuestaoOpcaoInputModelValidator : AbstractValidator<CriarAnamneseSubGrupoQuestaoOpcaoInputModel>
    {
        public CriarAnamneseSubGrupoQuestaoOpcaoInputModelValidator()
        {
            RuleFor(x => x.Texto).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o texto da opção da questão do subgrupo da anamnese!");            
        }
    }
}
