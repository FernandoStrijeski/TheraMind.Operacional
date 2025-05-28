using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseSgQuestaoO
    {
        [Key]
        public int ModeloAnamneseSgQuestaoOid { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public string? Texto { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ModeloAnamneseG ModeloAnamneseG { get; set; } = null!;
        public virtual ModeloAnamneseSg ModeloAnamneseSg { get; set; } = null!;
        public virtual ModeloAnamneseSgQuestao ModeloAnamneseSgQuestao { get; set; } = null!;
    }
}
