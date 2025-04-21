using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Estado
    {
        public Estado()
        {
            Cidades = new HashSet<Cidade>();
            Clientes = new HashSet<Cliente>();
        }

        [Key]  // Define a chave primária
        public string Uf { get; set; } = null!;
        public int PaisId { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime? DataCriacao { get; set; }

        public Pais Pais { get; set; } = null!;
        public virtual ICollection<Cidade> Cidades { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
