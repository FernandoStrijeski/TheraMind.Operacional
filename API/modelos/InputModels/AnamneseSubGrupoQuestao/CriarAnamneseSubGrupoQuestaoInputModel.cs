using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAnamneseSubGrupoQuestaoInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarAnamneseSubGrupoQuestaoInputModelValidator : AbstractValidator<CriarAnamneseSubGrupoQuestaoInputModel>
    {
        public CriarAnamneseSubGrupoQuestaoInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(250).WithMessage("Por favor, informe o título da questão do subgrupo da anamnese!");            
        }
    }
}
