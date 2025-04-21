using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Entidades
{
    public partial class TipoLogradouro
    {
        public TipoLogradouro()
        {
            Clientes = new HashSet<Cliente>();
            Filials = new HashSet<Filial>();
        }

        [Key]
        public string TipoLogradouroId { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
    }
}
