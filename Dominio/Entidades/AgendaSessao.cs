using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaSessao
    {
        public AgendaSessao()
        {
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
        }

        [Key]
        public Guid AgendaSessaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaId { get; set; }
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
        public DateTime? DataCriacao { get; set; }

        public virtual AgendaProfissional Agenda { get; set; } = null!;
        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual FormularioSessao FormularioSessao { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual Sala? Sala { get; set; }
        public virtual Servico Servico { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
    }
}
