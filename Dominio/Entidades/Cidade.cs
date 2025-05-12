using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Cidade
    {        
        [Key]
        public int CidadeId { get; set; }
        public int PaisId { get; set; }
        public string Uf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public int CodigoIbge { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Pais Pais { get; set; } = null!;
        public virtual Estado Estado { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Filial> Filiais { get; set; }
    }
}
