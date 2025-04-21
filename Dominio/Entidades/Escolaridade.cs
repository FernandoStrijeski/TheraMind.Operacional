using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Escolaridade
    {
        public Escolaridade()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]  // Define a chave primária
        public int EscolaridadeId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
