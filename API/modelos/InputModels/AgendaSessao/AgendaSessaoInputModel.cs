using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AgendaSessaoInputModel
    {
        /// <summary>
        /// Id da sessão na agenda
        /// </summary>
        public Guid AgendaSessaoId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Id do profissional responsável pela agenda
        /// </summary>
        public Guid ProfissionalId { get; set; }

        /// <summary>
        /// Id da agenda do profissional
        /// </summary>
        public int AgendaProfissionalId { get; set; }

        /// <summary>
        /// Id do serviço prestado
        /// </summary>
        public int ServicoId { get; set; }

        /// <summary>
        /// Id do formulário da sessão
        /// </summary>
        public int FormularioSessaoId { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Tipo de evento (0-Individual, 1-Grupo, 2-Geral)
        /// </summary>
        public short TipoEvento { get; set; }

        /// <summary>
        /// Tipo da modalidade (0-Presencial, 1-Online)
        /// </summary>
        public short Modalidade { get; set; }

        /// <summary>
        /// Id da sala
        /// </summary>
        public string? SalaId { get; set; }

        /// <summary>
        /// Horário de início da sessão
        /// </summary>
        public DateTime DataHoraInicio { get; set; }

        /// <summary>
        /// Horário de término da sessão
        /// </summary>
        public DateTime DataHoraFim { get; set; }

        /// <summary>
        /// Sessão dura o dia todo?
        /// </summary>
        public bool? DiaTodo { get; set; }

        /// <summary>
        /// Tipo de recorrência da agenda desse cliente (0-Nenhuma, 1-Semanal, 2-Quinzenal, 3-Mensal)
        /// </summary>
        public short TipoRecorrencia { get; set; }

        /// <summary>
        /// Data de térnimo da recorrência, caso exista
        /// </summary>
        public DateTime? RecorrenciaDataTermino { get; set; }

        /// <summary>
        /// Número de sessões recorrentes, caso exista
        /// </summary>
        public short? RecorrenciaNroSessoes { get; set; }

        /// <summary>
        /// Situação da sessão do cliente (0-Agendado, 1-Agendamento confirmado, 2-Paciente ausente, 3-Paciente Cancelou, 4-Paciente Presente, 5-Profissional Cancelou)
        /// </summary>
        public short Situacao { get; set; }

        /// <summary>
        /// O cliente efetuou o pagamento?
        /// </summary>
        public bool? PagamentoEfetuado { get; set; }

        /// <summary>
        /// Data de cancelamento da sessão, caso tenha sido cancelada
        /// </summary>
        public DateTime? DataCancelamento { get; set; }

        /// <summary>
        /// Motivo do cancelamento, caso tenha sido cancelada
        /// </summary>
        public string? MotivoCancelamento { get; set; }

        /// <summary>
        /// Manter a cobrança em caso de cancelamento?
        /// </summary>
        public bool? MantemCobranca { get; set; }

        /// <summary>
        /// Data de criação do registro no sistema
        /// </summary>
        public DateTime? DataCriacao { get; set; }
    }

    public class AgendaSessaoValidator : AbstractValidator<AgendaSessaoInputModel>
    {
        public AgendaSessaoValidator()
        {
                      
        }
    }
}
