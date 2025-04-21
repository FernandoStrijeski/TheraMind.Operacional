using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseSgquestao
    {
        public ModeloAnamneseSgquestao()
        {
            ModeloAnamneseSgquestaoOs = new HashSet<ModeloAnamneseSgquestaoO>();
        }

        [Key]
        public int ModeloAnamneseSgquestaoId { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ModeloAnamneseG ModeloAnamneseG { get; set; } = null!;
        public virtual ModeloAnamneseSg ModeloAnamneseSg { get; set; } = null!;
        public virtual ICollection<ModeloAnamneseSgquestaoO> ModeloAnamneseSgquestaoOs { get; set; }
    }
}
