using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAnamneseGrupoInputModel
    {
        public int AnamneseGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
     }

    public class CriarAnamneseGrupoInputModelValidator : AbstractValidator<CriarAnamneseGrupoInputModel>
    {
        public CriarAnamneseGrupoInputModelValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe o título da anamnese!");            
        }
    }
}
