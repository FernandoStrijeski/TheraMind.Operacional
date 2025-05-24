using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarFormularioSessaoCampoInputModel
    {
        public int FormularioSessaoCampoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public string NomeCampo { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
    }

    public class CriarFormularioSessaoCampoInputModelValidator : AbstractValidator<CriarFormularioSessaoCampoInputModel>
    {
        public CriarFormularioSessaoCampoInputModelValidator()
        {          
        }
    }
}
