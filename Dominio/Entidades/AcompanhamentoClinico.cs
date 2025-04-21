using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AcompanhamentoClinico
    {
        [Key]
        public Guid AcompanhamentoClinicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid? ClienteId { get; set; }
        public string? AvaliacaoDemanda { get; set; }
        public string? PlanoTratamento { get; set; }
        public string? Diagnostico { get; set; }
        public string? RegistroEncerramento { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
    }
}
