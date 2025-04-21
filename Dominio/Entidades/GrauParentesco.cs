using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class GrauParentesco
    {
        public GrauParentesco()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int GrauParentescoId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
