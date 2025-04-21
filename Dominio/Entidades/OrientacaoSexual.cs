using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class OrientacaoSexual
    {
        public OrientacaoSexual()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int OrientacaoSexualId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
