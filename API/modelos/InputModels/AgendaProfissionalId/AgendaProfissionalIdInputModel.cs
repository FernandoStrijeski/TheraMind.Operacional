using API.Core.Utils;
using FluentValidation;
using System.Text.Json.Serialization;

namespace API.modelos.InputModels
{
    public class AgendaProfissionalInputModel
    {
        /// <summary>
        /// Id da agenda do profissional
        /// </summary>
        public int AgendaProfissionalId { get; set; }

        /// <summary>
        ///  Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Id do profissional
        /// </summary>
        public Guid ProfissionalId { get; set; }

        /// <summary>
        /// Exibição da agenda em minutos para exibição agenda exemplo 5,10,15,20,25,30,35,40,45,50,55,60
        /// </summary>
        public int ExibicaoEmMinutos { get; set; }

        /// <summary>
        /// Duração de cada sessão em minutos 
        /// </summary>
        public int DuracaoSessaoMinutos { get; set; }

        /// <summary>
        /// Hora de início da agenda
        /// </summary>        
        public TimeSpan HoraInicio { get; set; }

        /// <summary>
        /// Hora de fim da agenda
        /// </summary>
        public TimeSpan HoraFim { get; set; }

        /// <summary>
        /// Relação de dias não exibidos na agenda, separados por virgula. exemplo "Domingo e Sábado" = 0,6
        /// </summary>
        public string? DiasOcultados { get; set; }

        /// <summary>
        /// Exibe na agenda sessões ausentes ou canceladas
        /// </summary>
        public bool? ExibeSessoesAusentesCanc { get; set; }

        /// <summary>
        /// Exibe na agenda informação de comparecimento ou não comparecimento do cliente na sessão
        /// </summary>
        public bool? ExibeComparecimento { get; set; }

        /// <summary>
        /// Exibe na agenda a informação do pagamento se já efetuado ou pendente
        /// </summary>
        public bool? ExibePagamento { get; set; }

        /// <summary>
        /// Exibe na agenda os dias de feriados nacionais
        /// </summary>
        public bool? ExibeFeriadosNacionais { get; set; }

        /// <summary>
        /// Tipo de visualização da agenda pelo profissional -- 0-Mensal, 1-Semanal, 2-Diaria
        /// </summary>
        public short TipoVisualizacao { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class AgendaProfissionalValidator : AbstractValidator<AgendaProfissionalInputModel>
    {
        public AgendaProfissionalValidator()
        {                   
        }
    }
}
