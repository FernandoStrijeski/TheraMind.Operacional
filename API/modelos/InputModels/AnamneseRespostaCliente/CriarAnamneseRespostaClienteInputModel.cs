using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAnamneseRespostaClienteInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AnamneseGrupoId { get; set; }
        public int AnamneseSubGrupoId { get; set; }
        public int AnamneseSubGrupoQuestaoId { get; set; }
        public Guid ClienteID { get; set; }
        public string? Resposta { get; set; }
    }

    public class CriarAnamneseRespostaClienteValidator : AbstractValidator<CriarAnamneseRespostaClienteInputModel>
    {
        public CriarAnamneseRespostaClienteValidator()
        {

        }
    }
}
