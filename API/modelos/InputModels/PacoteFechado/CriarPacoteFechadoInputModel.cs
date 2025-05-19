using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarPacoteFechadoInputModel
    {
        public int PacoteFechadoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int QuantidadeSessoes { get; set; }
        public decimal ValorTotal { get; set; }
        public bool? Ativo { get; set; }
    }

    public class CriarPacoteFechadoInputModelValidator : AbstractValidator<CriarPacoteFechadoInputModel>
    {
        public CriarPacoteFechadoInputModelValidator()
        {
        }
    }
}
