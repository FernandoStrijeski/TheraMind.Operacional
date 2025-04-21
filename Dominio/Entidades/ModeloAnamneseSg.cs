using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseSg
    {
        public ModeloAnamneseSg()
        {
            ModeloAnamneseSgquestaoOs = new HashSet<ModeloAnamneseSgquestaoO>();
            ModeloAnamneseSgquestaos = new HashSet<ModeloAnamneseSgquestao>();
        }

        [Key]
        public int ModeloAnamneseSgid { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ModeloAnamneseG ModeloAnamneseG { get; set; } = null!;
        public virtual ICollection<ModeloAnamneseSgquestaoO> ModeloAnamneseSgquestaoOs { get; set; }
        public virtual ICollection<ModeloAnamneseSgquestao> ModeloAnamneseSgquestaos { get; set; }
    }
}
