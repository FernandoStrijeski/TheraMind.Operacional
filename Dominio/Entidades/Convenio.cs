using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Convenio
    {
        public Convenio()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int ConvenioId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public short TipoRepasse { get; set; }
        public decimal ValorRepasse { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
