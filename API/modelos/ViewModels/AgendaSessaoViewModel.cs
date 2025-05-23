using Dominio.Entidades;

namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class AgendaSessaoViewModel
    {
        public Guid AgendaSessaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }

        public virtual ServicoViewModel Servico { get; set; } = null!;
        public int FormularioSessaoId { get; set; }
        //public virtual FormularioSessaoViewModel FormularioSessao { get; set; } = null!;
        public Guid? ClienteId { get; set; }
        public virtual ClienteViewModel Cliente { get; set; } = null!;
        public short TipoEvento { get; set; }
        public short Modalidade { get; set; }
        public string? SalaId { get; set; }
        public virtual SalaViewModel Sala { get; set; } = null!;
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
        //public virtual ICollection<AgendaSessaoItemViewModel> AgendaSessaoItens { get; set; }
    }
}
