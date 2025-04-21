using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseSgquestaoO
    {
        [Key]
        public int ModeloAnamneseSgquestaoOid { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseSgquestaoId { get; set; }
        public string? Texto { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ModeloAnamneseG ModeloAnamneseG { get; set; } = null!;
        public virtual ModeloAnamneseSg ModeloAnamneseSg { get; set; } = null!;
        public virtual ModeloAnamneseSgquestao ModeloAnamneseSgquestao { get; set; } = null!;
    }
}
