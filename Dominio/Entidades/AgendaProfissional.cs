using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaProfissional
    {
        public AgendaProfissional()
        {
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
        }

        [Key]
        public int AgendaId { get; set; }
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

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
    }
}
