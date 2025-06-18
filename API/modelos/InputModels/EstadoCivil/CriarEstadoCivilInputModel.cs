using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEstadoCivilInputModel
    {
        public string EstadoCivilId { get; set; }
        public string Descricao { get; set; } = null!;        
    }

    public class CriarEstadoCivilInputModelValidator : AbstractValidator<CriarEstadoCivilInputModel>
    {
        public CriarEstadoCivilInputModelValidator()
        {
            RuleFor(p => p.EstadoCivilId)
            .NotNull().WithMessage("O código não pode ser nulo!")
            .NotEmpty().WithMessage("O código é obrigatório!")
            .MaximumLength(1).WithMessage("O código deve conter no maximo 1 caracter!");

            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a descrição do estado civil!");            
        }
    }
}
