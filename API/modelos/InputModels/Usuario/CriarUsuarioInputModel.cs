using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarUsuarioInputModel
    {
        public Guid? EmpresaId { get; set; }
        public int? FilialId { get; set; }
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public bool TrocaSenhaProximoAcesso { get; set; }
        public string PerfilAcesso { get; set; } = null!;
        public bool? Ativo { get; set; }        
    }

    public class CriarUsuarioInputModelValidator : AbstractValidator<CriarUsuarioInputModel>
    {
        public CriarUsuarioInputModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(2500).WithMessage("Por favor, informe o e-mail utilizado para login!");            
        }
    }
}
