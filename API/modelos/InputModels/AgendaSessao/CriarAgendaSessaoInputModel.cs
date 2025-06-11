using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAgendaSessaoInputModel
    {
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }       
        public Guid? ClienteId { get; set; }
        public short TipoEvento { get; set; }
        public short Modalidade { get; set; }
        public string? SalaId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public bool? DiaTodo { get; set; }
        public short TipoRecorrencia { get; set; }
        public DateTime? RecorrenciaDataTermino { get; set; }
        public short? RecorrenciaNroSessoes { get; set; }
        public short Situacao { get; set; }
        public bool? PagamentoEfetuado { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string? MotivoCancelamento { get; set; }
        public bool? MantemCobranca { get; set; }
    }

    public class CriarAgendaSessaoInputModelValidator : AbstractValidator<CriarAgendaSessaoInputModel>
    {
        public CriarAgendaSessaoInputModelValidator()
        {

        }
    }
}
