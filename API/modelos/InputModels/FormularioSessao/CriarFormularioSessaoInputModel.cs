using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarFormularioSessaoInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
    }

    public class CriarFormularioSessaoInputModelValidator : AbstractValidator<CriarFormularioSessaoInputModel>
    {
        public CriarFormularioSessaoInputModelValidator()
        {          
        }
    }
}
