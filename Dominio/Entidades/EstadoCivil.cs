using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class EstadoCivil
    {
        public EstadoCivil()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public string EstadoCivilId { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
