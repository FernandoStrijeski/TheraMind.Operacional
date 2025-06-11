using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEstadoInputModel
    {
        public string UF { get; set; }

        public int PaisID { get; set; }

        public string Descricao { get; set; } = null!;        
    }

    public class CriarEstadoInputModelValidator : AbstractValidator<CriarEstadoInputModel>
    {
        public CriarEstadoInputModelValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty().MaximumLength(100).WithMessage("Por favor, informe a descrição do estado!");            
        }
    }
}
