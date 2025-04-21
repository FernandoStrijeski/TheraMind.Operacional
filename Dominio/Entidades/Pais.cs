using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Pais
    {
        public Pais()
        {
            Cidades = new HashSet<Cidade>();
            Clientes = new HashSet<Cliente>();
            Estados = new HashSet<Estado>();
        }

        [Key]  // Define a chave primária
        public int PaisId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public ICollection<Cidade> Cidades { get; set; }
        public ICollection<Estado> Estados { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
