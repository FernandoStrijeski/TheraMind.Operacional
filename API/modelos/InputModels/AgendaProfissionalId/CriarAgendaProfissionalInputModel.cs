using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarAgendaProfissionalInputModel
    {
        public int AgendaProfissionalId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int ExibicaoEmMinutos { get; set; }
        public int DuracaoSessaoMinutos { get; set; }

        public TimeSpan HoraInicio { get; set; }


        public TimeSpan HoraFim { get; set; }
        public string? DiasOcultados { get; set; }
        public bool? ExibeSessoesAusentesCanc { get; set; }
        public bool? ExibeComparecimento { get; set; }
        public bool? ExibePagamento { get; set; }
        public bool? ExibeFeriadosNacionais { get; set; }
        public short TipoVisualizacao { get; set; }
        public DateTime? DataCriacao { get; set; }
    }

    public class CriarAgendaProfissionalIdInputModelValidator : AbstractValidator<CriarAgendaProfissionalInputModel>
    {
        public CriarAgendaProfissionalIdInputModelValidator()
        {      
        }
    }
}
