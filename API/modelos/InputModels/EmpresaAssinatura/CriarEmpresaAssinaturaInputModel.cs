using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEmpresaAssinaturaInputModel
    {
        public Guid EmpresaAssinaturaId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid PlanoId { get; set; }
        public short TipoPlano { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal? DescontoPromocional { get; set; }
        public short? DescontoMeses { get; set; }
        public DateTime? DataExpiracao { get; set; }        
        public bool? Ativo { get; set; }
    }

    public class CriarEmpresaAssinaturaInputModelValidator : AbstractValidator<CriarEmpresaAssinaturaInputModel>
    {
        public CriarEmpresaAssinaturaInputModelValidator()
        {
        }
    }
}
