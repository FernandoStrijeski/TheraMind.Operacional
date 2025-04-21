using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Sala
    {
        public Sala()
        {
            AgendaSessaos = new HashSet<AgendaSessao>();
        }

        [Key]
        public string SalaId { get; set; } = null!;
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
    }
}
